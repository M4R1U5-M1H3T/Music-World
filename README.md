# 🎵 Lumea Muzicii (Music World)

A Windows Forms desktop application built in C# for managing and playing local MP3 files. This application features user authentication and isolated, personalized music libraries for each registered user. 

*Note: This project was developed as my high school graduation project (Atestat) in Computer Science.*

## 🚀 Key Features

* **User Authentication:** Secure login and registration system with database integration and password masking.
* **Personalized Libraries:** The system automatically generates unique directories based on User IDs to keep each user's uploaded MP3 files isolated and private.
* **Audio Player Controls:** Full control over music playback including Play, Pause, Resume, and a custom Shuffle algorithm for playlists.
* **Interactive Tutorial:** Integrated video guide using `axWindowsMediaPlayer` to help new users navigate the application.
* **Local Database:** Uses SQL Server Compact for lightweight, local data management of user credentials and file paths.

## 🛠️ Tech Stack

* **Language:** C#
* **Framework:** Windows Forms (.NET Framework)
* **Database:** SQL Server Compact 3.5 
* **IDE:** Microsoft Visual Studio 2010

## 🧠 What I Learned

Building this project was my first deep dive into full-stack desktop development. Key takeaways include:
* Understanding the connection between a Graphical User Interface (GUI) and a relational database.
* Managing the local file system using C# `System.IO` to dynamically copy, store, and delete user files.
* Handling state across multiple Windows Forms and managing media playback states.

## 🛠️ Uncommon Issue!

For some reason, Form1.resx, Form2.resx, Form3.Resx might not be accessible at first.
They're added in a separate folder named ResxFiles.
Right-click on the .resx file, properties -> unblock -> apply.
The files from the project have to be replaced by these.