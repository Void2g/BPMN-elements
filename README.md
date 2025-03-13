# 3D BPMN Elements for Unity

The BPMN Elements asset package for Unity is designed to help visualize Business Process Model and Notation (BPMN) diagrams in 3D.

This package includes prefabs that represent BPMN elements such as tasks, events, and gateways. It also contains a flow prefab with a script to connect elements at runtime.

It is important to note that this package only contains the visualization of BPMN elements and does not include any execution or import logic for the processes.

## Table of Contents
1. [Project Structure](#project-structure)
2. [Element Setup](#element-setup)
3. [Flow Setup](#flow-setup)
4. [AR/VR Integration](#arvr-integration)
5. [Scripts](#scripts)
    - [LineCreation](#linecreation)
    - [LabelPositioning](#labelpositioning)
    - [FaceCamera](#facecamera)
6. [Sample Scene](#sample-scene)

## Project Structure
The package is structured as follows:

```
BPMN elements/
├── Dependencies/
│   ├── Arrowhead.prefab
│   ├── BillboardLabel.prefab
│   ├── ConeMesh.asset
│   ├── Materials/
│   │   ├── EndEvent.mat, StartEvent.mat, LabelBackground.mat, etc.
│   │   ├── Textures for BPMN elements
│   ├── ModelElement.prefab
│   ├── Scripts/
│   │   ├── FaceCamera.cs
│   │   ├── LabelPositioning.cs
│   │   ├── LineCreation.cs
├── Flow.prefab
├── EndEvent Variant.prefab
├── Parallel Variant.prefab
├── StartEvent Variant.prefab
├── Task Variant.prefab
├── Xor Variant.prefab
├── Sample/
│   ├── SampleBPMN.unity (Example scene)
├── README.md
```

## Element Setup

The different variants (EndEvent, Parallel, StartEvent, Task, Xor) can be used to place the corresponding BPMN elements in your scene. Each variant is a prefab that represents a specific BPMN element type.

Texts inside of the prefabs can always be found with `GetComponentsInChildren<TextMeshPro>()`.

New variants can be created from the **ModelElement.prefab** or existing variants.

## Flow Setup
- To set up flows, place the **Flow.prefab** in the scene.
- Assign the **startObject** and **endObject** in the **LineCreation** script attached to the prefab.
- The script will automatically create the flow between the two objects.
- Model element prefab variants can be modified by removing markers to limit where sequence flows can attach.

## AR/VR Integration
- To enable AR/VR compatibility, uncomment the lines in the scripts that use the `OVRCameraRig`.
- Tipp: the main BPMN element prefab can have **grabbing and interaction components** from the Meta Interaction SDK added for AR/VR interaction.

## Scripts

The included scripts are needed for the prefabs to work correctly. They are not meant to be used on their own.

### LineCreation
**Description:**
- Creates a line between two BPMN elements.
- Uses markers of the Element Prefabs to determine the optimal connection points.
- Positions the arrowhead at the end and places a label in the middle.

### LabelPositioning
**Description:**
- Ensures that BPMN labels face the camera.
- Moves the label to the top or bottom of the BPMN element based on the camera's position.

### FaceCamera
**Description:**
- Rotates the GameObject to always face the camera.
- Can be used for billboards and floating labels.

## Sample Scene
- The **SampleBPMN.unity** scene provides an example setup of BPMN elements with flow connections.
- It demonstrates how to use the prefabs and scripts to create a BPMN diagram.
