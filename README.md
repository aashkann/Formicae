# Formicae: Bridging Forma and Rhino

Formicae is a project born out of the Forma Hackathon, hosted by Autodesk's Forma team at Autodesk's Oslo office on February 13-14, 2024. This project establishes a connection between Forma and Rhino, focusing on the integration of wind prediction analyses into the design process directly to Rhino.

![Animation](https://github.com/aashkann/Formicae/assets/101568776/bb87a302-d802-4dbc-a958-b025da943739)

## Features

- **3-Legged Authentication**: Ensures secure access and data exchange between Forma and Rhino, leveraging robust authentication protocols to protect your projects.
- **Streamlined Connectivity**: Facilitates a direct and efficient link from Rhino to Forma and vice versa, simplifying the workflow for architects and designers.
- **Wind Prediction Analysis**: Incorporates advanced wind analysis directly into your design workflow, enabling data-driven decision-making for architectural projects. Note that we are still calibrating the results one gets in Rhino with what Forma gives. The results per such are still not ready. 



## Prerequisites

Before you begin with Fomicae, there are a couple of requirements to ensure a smooth setup and operation:

- **Rhino Installation**: Fomicae is developed as a plugin for Grasshopper, which requires a functioning copy of Rhino. Ensure you have Rhino installed on your system. The plugin is developed for R7. R8 is still not tested.

- **Swiftlet Integration**: For the initial implementation, Fomicae integrates with Swiftlet, a tool designed to enhance Grasshopper's capabilities. While Swiftlet is an external dependency at present, we are exploring the possibility of making it a native feature in future developments. [Download](https://www.food4rhino.com/en/app/swiftlet)

- **jSwan**: Parsing respond json. [Download](https://www.food4rhino.com/en/app/jswan)

Please ensure these prerequisites are met to fully leverage Fomicae's functionalities.


## Loading Extension

You can use Formicae extension id: "b8b53625-9454-4330-8cee-893098f21803" to add it to your project.

## How to test it?
<span style="color:red;"><strong>Caution: This is an experimental project aimed at exploring the potential integration between Rhino and Autodesk Forma. Please note that it is not yet suitable for use in production environments or as a primary tool in your project analysis. However, we welcome your interest and encourage you to follow our progress. If you're interested in contributing, please don't hesitate to get in touch.</strong></span>

0 - Make sure you have all the prequisites from "Prerequisites" section.

1 - Clone this repo on your computer and Copy ".\bin\Debug\net48" files to your grasshopper plugin location, in a new folder called Formicae like ".\Grasshopper\Libraries\Formicae"

2 - Open ".\Sample\sample.3dm" and load ".\Sample\forma-api.gh" in Grasshopper

3 - Copy all DLLs from "Dependencies" folder into "Formicae" Grasshopper folder 

4 - In the test Grasshopper script "forma-api.gh":

- Locate the Python node with message "authcontext", and replace set variable "url" to URL of the Forma project where the Formicae extension is installed. This ensures that the correct region ("us" vs "eu") and Project ID are used.
	
- Locate the Python node with message "x-ads-region", and set the value to a string consistent with the "url" above - "US" for ".com" and "EMEA" for ".eu".

- Authentificate by pressing the button and there you have it.


## Contribution

Fomicae is an open-source project, and we welcome contributions from the community. Whether you're a developer, a designer, or an architect, your insights and improvements can help shape the future of Fomicae. We aim to set up a robust guidlines for this as soon as possible.

## Team member
- **Ashkan Rezaee**
- **Mikael Spinnagr**
- **Philipp Zimmerman**
- **Joel Yngvesson**

<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

