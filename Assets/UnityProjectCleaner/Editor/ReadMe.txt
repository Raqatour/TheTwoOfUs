Known Issues
--------------
***Cannot get base class reference if the base class isn't inside a scene or resource folder.
***Using "Find Inactive Object" under Find Reference will return all objects active and inactive but will also return objects that may not be in use in any scene.


Instructions for Unused Assets (Beta v0.7)
-----------------------------------------------------------
1 - Import the Unity Project Cleaner(UPC) into your project.

2 - Go to Window/Unity Project Cleaner/Find Unused Assets

3 - Click "Get Folder Paths", this will output a list of all the folder paths in your project that contain assets. It will not include empty folders.

4 - Uncheck any folder path that you don't want UPC to look at. So if you have a Testing folder with assets that you know aren't being used, but you will in the 
future, just uncheck that folder path and UPC will not look into that folder for any unused assets.

5 - Click "Get All Assets" at the bottom. This will now go though and find all assets inside your project that are not inside of any Resources, Editor, Plugins, Temp, Standard Assets or folders
you have excluded. This will take a little bit of time depending on the size of your project.

6 - Once all the assets are found you have a couple of options at the top. 
6a - Use the "Hide Used Assets" toggle in the top right to hide everything that is being used in the project.
6b - Use the "Find Inactive Gameobject" toggle to find objects in the scenes that are inactive. Keep this uncheck to only find objects inside of the scenes that are active.
6c - "Find Inactive GameObjects" return more than it should. It will return all models, prefabs even if they aren't being used in any scene.

7 - Check/Uncheck scenes you want to run the scan against(You can uncheck demo scenes that you may of been testing with). You can also use the Uncheck/ Check All button
to quickly turn on/off all the scenes to test.

8 - Press "Scan" and wait for the output.

9 - Go through the list and check anything you want to keep and check it. You can use the Sort By drop down to sort objects by scripts, shaders, material, etc.

10 - Press "Move all Trash" this will put all the assets into a "Temp" folder and you can delete the asset yourself.




Instructions for File Structure
-----------------------------------------------------------
1 - Import the Unity Project Cleaner(UPC) into your project.

2 - Go to Window/Unity Project Cleaner/File Structure

3 - Click "Sort Project"

4 - A message will appear, press "Yes" to sort the project(once it is sorting, it cannot be undone)

5 - All your assets will now be put into their proper place.

6 - Any unknown file types will be imported into the "Misc" folder.




Future Features List
--------------------------------------------------------
***A simple to use, node based editor to sort your project files with.
***A tagging system to better sort files.
***Faster performence for everything.
***More things as I think of them. 