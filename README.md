# ENGR248 Solidworks Submission Form
A Windows Form Application to help TAs (like myself) speed up their grading process of Solidworks parts, which are submitted by Oregon State University students in ENGR 248 ("Engineering Graphics and 3D Design"). This project uses Windows Form Applications with the Solidworks API - written in C#.

This Form shows student folders, their parts, and each part's sketch as nodes within a TreeView. This could greatly speed up workflow when grading, since it removes the need to navigate the filesystem and Solidworks dialogs. To begin, select a root directory containing student submission folders (eg. downloaded from Canvas and unzipped).

## Download .exe
<a href="https://github.com/Justin-Mai/SolidworksSubmissionsForm/raw/master/WindowsFormsApplication1/bin/Release/app.publish/WindowsFormsApplication1.exe">Download the current (WIP) .exe here</a>

(found in WindowsFormsApplication1\bin\Release\app.publish)

## Screenshot  (probably outdated) 
![Form screenshot, work in progress](https://i.imgur.com/9NsfR0k.png "Form screenshot (WIP)")

## TO-DO List
Future goals for this Form include:
 - Opening sketches in Normal-To view, from the TreeView
 - Adding functionality to "Previous"/"Next" increment buttons
 - Scraping data from features (timestamps, owner, # of undefined sketches, etc.)
 - Saving/exporting scraped data for upload into spreadsheet
 - Dimension-checking (ie. user can specify correct dimensions, then select contours within Solidworks to verify proper dimensions)
 - Determining "true" undefined sketches (vs. sketches with an undefined but unused point/line)
