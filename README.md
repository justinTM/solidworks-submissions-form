# ENGR248 Solidworks Submission Form
A Solidworks API Windows Form Application (in C#) to help TAs (like myself) speed up their grading process of Solidworks parts. These parts are submitted by Oregon State University students in ENGR 248 ("Engineering Graphics and 3D Design"). 

This Form App shows a TreeView containing, as nodes:
 - student submission folders
   - each part submitted by this student
     - each sketch within the part. 

## Purpose
The goal is for this project to speed up grading workflow and making grading less monotonous for TAs by:  
- removing the need to navigate though the filesystem
- reducing the number of clicks required by the grader
- iterating through student submissions with simple controls

It could also provide students with higher-quality feeback, since less time can be spent on navigation or other overhead, and instead more time giving detailed feedback.

## How to run / compile
<a href="https://github.com/Justin-Mai/SolidworksSubmissionsForm/raw/master/WindowsFormsApplication1/bin/Release/app.publish/WindowsFormsApplication1.exe">An executable (.exe) is included</a> automatically in this repo (found in WindowsFormsApplication1\bin\Release\app.publish).

You can also build it yourself simply by opening the Visual Studio Solution (.sln) file, setting the Build Configuration to "Release" or "Debug" and hitting the green "Start" button within Visual Studio.

## How to use
Coming soon...

## Screenshot  (probably outdated) 
![Form screenshot, work in progress](https://i.imgur.com/9NsfR0k.png "Form screenshot (WIP)")

## TO-DO List
Feature goals for this Form include:
 - ~~Opening sketches in Normal-To view, from the TreeView~~ **Done!**
 - Adding functionality to "Previous"/"Next" increment buttons
 - Scraping data from features (timestamps, owner, # of undefined sketches, etc.)
 - Saving/exporting scraped data for upload into spreadsheet
 - Dimension-checking (ie. user can specify correct dimensions, then select contours within Solidworks to verify proper dimensions)
 - Determining "true" undefined sketches (vs. sketches with an undefined but unused point/line)
