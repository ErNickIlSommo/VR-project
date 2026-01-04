# VR Project - Group 7
## Setup

- Unity Version: 6.2.xf2
- Meta XS SDK: 
- Unity packages:


Currently, we are going to develop our application with **Unity OpenXR**. To use it, some components are needed:
1. Open Unity Hub
2. Go to **installs** section which displays your Unity Editor Versios.
3. Install an editor if the section is empty.
4. Next, click the settings icon (gear symbol) \> **Manage** \> Add Modules
5. Select the following componenets
    - **Android Build Support**
    - OpenJDK
    - Android SDK & NDK Tools


## Warning

### Ambient Occlusion
By going to `Project Settings > XR Plug-in Management > Project Validation occurs` if a warning that talks about overhead due to ambient occlusion we can fix by doing the following steps:
1. Go to Project Tab in Unity
2. Go to Settings folder
3. Click `PC_Renderer`; the settings will appear in the Inspector Tab
4. Right Click `Screen Space Ambient Occlusion` and then select `Remove`

## Things to do
