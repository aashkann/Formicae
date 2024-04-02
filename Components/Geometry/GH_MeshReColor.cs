using System;
using System.Drawing;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Rhino.Geometry;
using Grasshopper.GUI.Gradient;
using System.Linq;
using Rhino.Render.ChangeQueue;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Formicae.Components.Geometry
{
    public class GH_MeshReColor : GH_Component
    {
        public override Guid ComponentGuid => new Guid("445891C9-D9A5-4931-B100-E1AC44D3B52A");
        protected override Bitmap Icon => null;
        public GH_MeshReColor()
          : base("meshReColor", "meshReColor",
              "meshReColor",
              Config.FormicaeTab, Config.Tabs.Geometry)
        {
        }

        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddMeshParameter("Mesh", "M", "Mesh to recolor", GH_ParamAccess.item);
            pManager.AddNumberParameter("Values", "Vals", "Values with the same number of faces as mesh",
                GH_ParamAccess.list);
            pManager.AddNumberParameter("minValue", "minVal", "Default minValue: 0.0", GH_ParamAccess.item);
            pManager.AddNumberParameter("maxValue", "maxVal", "Default maxValue: 1.0", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager[3].Optional = true;
        }


        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddMeshParameter("reColoredmesh", "reCesh", "reColoredmesh", GH_ParamAccess.item);
        }


        protected override void SolveInstance(IGH_DataAccess DA)
        {

            Rhino.Geometry.Mesh inputMesh = new Rhino.Geometry.Mesh();
            List<double> values = new List<double>();
            double clampMin = 0.0;
            double clampMax = 1.0;

            if (!DA.GetData(0, ref inputMesh)) return;
            if (!DA.GetDataList(1, values)) return;
            DA.GetData(2, ref clampMin);
            if (!DA.GetData(3, ref clampMax))
            {
                clampMax = values.Max();
            }



            inputMesh.Compact();
            inputMesh.Normals.ComputeNormals();
            inputMesh.VertexColors.Clear();

            if (inputMesh.Faces.Count != values.Count)
            {
                AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Number of mesh faces must be equal to the number of values.");
                return;
            }

            var gradientColors = new List<Color>
            {
                Color.FromArgb(255, 178, 248, 218),
                Color.FromArgb(255, 85, 220, 162),
                Color.FromArgb(255, 254, 213, 42),
                Color.FromArgb(255, 254, 168, 0),
                Color.FromArgb(255, 177, 25, 15)
            };

            double[] vertexValues = new double[inputMesh.Vertices.Count];


            var gradients = Gradients(gradientColors.ToArray(), clampMin, clampMax);
            Rhino.Geometry.Mesh outputMesh = new Rhino.Geometry.Mesh();
            var numbers = Enumerable.Range(0, inputMesh.Faces.Count());
            var _values = numbers.AsParallel().AsOrdered();

            var faceData = new ConcurrentBag<(int faceIndex, Color cf, MeshFace face)>();

            Parallel.ForEach(_values, (i, state, index) =>
            {
                var cf = gradients.ColourAt(values[i]);
                MeshFace face = inputMesh.Faces[i];
                faceData.Add((i, cf, face));
            });


            var orderedFaceData = faceData.OrderBy(x => x.faceIndex).ToList();

            int faceCounter = 0;
            foreach (var data in orderedFaceData)
            {
                var (faceIndex, cf, face) = data;


                outputMesh.Vertices.Add(inputMesh.Vertices[face.A]);
                outputMesh.Vertices.Add(inputMesh.Vertices[face.B]);
                outputMesh.Vertices.Add(inputMesh.Vertices[face.C]);

                outputMesh.VertexColors.Add(cf);
                outputMesh.VertexColors.Add(cf);
                outputMesh.VertexColors.Add(cf);

                if (face.IsQuad)
                {
                    outputMesh.Vertices.Add(inputMesh.Vertices[face.D]);
                    outputMesh.VertexColors.Add(cf);
                    outputMesh.Faces.AddFace(faceCounter, faceCounter + 1, faceCounter + 2, faceCounter + 3);
                    faceCounter += 4;
                }
                else { 
                    outputMesh.Faces.AddFace(faceCounter, faceCounter + 1, faceCounter + 2);
                faceCounter += 3;
            }
        }



        DA.SetData(0, outputMesh);
        }
    private GH_Gradient Gradients(Color[] colorarray, double start, double end)
    {
        GH_Gradient gradient2 = new GH_Gradient();
        for (int i = 0; i < colorarray.Count(); i++)
        {
            double grip =
                start + (double)i * (end - start) /
                (colorarray.Count() - 1);
            gradient2.AddGrip(grip, colorarray[i]);
        }

        return gradient2;
    }

}
}