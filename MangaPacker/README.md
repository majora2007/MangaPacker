# Manga Packer
This is a very simple utility program to pack manga scan rips from mangadex into 
cbz files. 

## How to run
MangaPackerApp.exe "full path to directory to scan"

You need quotes if your path has spaces in it. 

## How it works
This program is designed around mangadex-scraper, so it expects folders, like:
<i>Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)</i>
that have Series, Volume, and optionally chapter information in them.

Inside, images should be 0002.jpg, 0003.jpg, etc. These will be renamed as:
Killing Bites - v1 - ch. 001 - pg. 0002.jpg and be zipped up.