Install

EntityFramework:

core

design

tools

MySQL.EntityFrameWorkCore

<img src="media/image1.png" style="width:2.02112in;height:3.09418in" />

<img src="media/image2.png" style="width:4.07174in;height:3.67708in" />update
this + foreign key

<img src="media/image3.png" style="width:3.97972in;height:3.69843in" />

<img src="media/image4.png" style="width:2.73958in;height:0.64583in" />
(primary key tutorial)

update this

<img src="media/image5.png" style="width:5.88624in;height:3.2192in" />

Így lesz 1:N kapcsolat 🡺 1 book = 1 author

ÚJ folder (data)

<img src="media/image6.png" style="width:2.82331in;height:3.06293in" />

<img src="media/image7.png" style="width:2.72955in;height:0.46882in" />

Sima Class létrehozása

Ez a class codeja

<img src="media/image8.png" style="width:5.42408in;height:2.61458in" />

appsettings.json beállítások (top rész):

<img src="media/image9.png" style="width:6.3in;height:1.83056in" />

Program.cs kiegészítés(top rész):

<img src="media/image10.png" style="width:4.875in;height:3.55576in" />

<img src="media/image11.png" style="width:6.3in;height:7.65069in" />

Majd ezt beírni ide

<img src="media/image12.png" style="width:4.95833in;height:1.32102in" />

Majd beírjuk „update-database” ugyan ide a package manager console-ba

<img src="media/image13.png" style="width:6.3in;height:5.81319in" />

xampp preview

e<img src="media/image14.png" style="width:6.3in;height:5.49514in" />

ez -\> api -\> entity frameworkos cucc

<img src="media/image15.png" style="width:6.3in;height:2.58194in" />

Ez az Author Controller

utana ezt berakni az authorba

<img src="media/image16.png" style="width:5.58411in;height:3.19836in" />

JsonIgnore a foreign key felé

<img src="media/image17.png" style="width:3.37547in;height:0.91679in" />

**DTO PROJEKT INNENTŐL!!!!!!!!**

2 új mappa

<img src="media/image18.png" style="width:1.95861in;height:0.65634in" />

classek megcsinalva

<img src="media/image19.png" style="width:1.95861in;height:1.81275in" />

ReadDto, UpdateDto

<img src="media/image20.png" style="width:4.15296in;height:2.30208in" />

CreateDto

<img src="media/image21.png" style="width:4.6569in;height:2.48993in" />

BookReadDto

<img src="media/image22.png" style="width:5.54244in;height:3.92763in" />

CreateDto

<img src="media/image23.png" style="width:4.35477in;height:3.35463in" />

UpdateDto

<img src="media/image24.png" style="width:4.30208in;height:3.5217in" />

AuthorController átírása (kell a namespace-be a mappa\[IEnumerable is
átírva\])

<img src="media/image25.png" style="width:6.3in;height:2.82292in" />

GetByID

<img src="media/image26.png" style="width:6.18836in;height:3.7922in" />

Put (update-elt kód)

<img src="media/image27.png" style="width:6.3in;height:5.4125in" />

PostDto update

<img src="media/image28.png" style="width:6.3in;height:2.86389in" />

BookReadDto

<img src="media/image29.png" style="width:6.3in;height:3.05278in" />

GetByID

<img src="media/image30.png" style="width:6.3in;height:4.24306in" />

Put

<img src="media/image31.png" style="width:6.3in;height:6.03264in" />

Post

<img src="media/image32.png" style="width:6.3in;height:3.9125in" />

**Delete ugyanaz mind2-nél (no change)**
