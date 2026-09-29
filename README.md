# Wretch Album Backup Downloader

A small WinForms tool I wrote in September 2013 to download every photo from a 無名小站 (Wretch) album backup before the site shut down.

**Status: archived historical project, no longer maintained.** Wretch closed on 26 December 2013, so there is nothing left to download.

## Background

無名小站 (Wretch, wretch.cc) was one of Taiwan's most popular blog and photo album sites in the 2000s, later owned by Yahoo! Taiwan. On 30 August 2013 Yahoo! announced that Wretch would close on 26 December 2013, and from 2 September users could request a backup download. The backup's `album` folder contained a `detail.html` index and text files listing each album's photo URLs, and the photos themselves still had to be fetched from Wretch. This tool read those files and downloaded all the photos, one folder per album, before the links went dead.

## How it was used

The Chinese manual `WretchBackup 使用說明.docx` describes the steps:

1. Sign in to Wretch and request the backup (at `download.wretch.cc/eol/download`, or through 開始備份/下載/搬家 on wretch.cc).
2. Download the zip and extract its `album` folder.
3. Open WretchBackup.exe and select `detail.html` in the `album` folder.
4. Click 下載 (Download). Each album is saved into its own folder inside `album`.

## Contents

- `WretchAlbumBackupDownload/MainForm.cs`: parsing and download logic, run on a background thread.
- `WretchAlbumBackupDownload/dlls/HtmlAgilityPack.dll`: the HTML Agility Pack library, used to parse `detail.html`.
- `WretchBackup 使用說明.docx`: the user manual.

## Cleaned before publication

Build output (`bin/`, `obj/`) and Visual Studio user files (`*.suo`, `*.user`) are removed. Nothing else needed cleaning.

## License

No open-source license applies to my own code. Do not assume the repository grants any license. `HtmlAgilityPack.dll` belongs to the HTML Agility Pack project and keeps its own license.
