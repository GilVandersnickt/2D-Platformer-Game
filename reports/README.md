# Innovative Project: Reports

## **Inhoudstafel**
[Research](#research)  
[Voorbereidingen](#voorbereidingen)  
[Opbouw](#opbouw)  
[Links](#links)

## Research 
- PRI Unity pdf:
  - Ik heb de tutorial gevolgd van in de module **PRI** om een eerste kennismaking te hebben met Unity. Dit was een tutorial voor een 3D-game maar ik heb er veel van kunnen gebruiken voor mijn 2D-game.
- Unity:
  - Ik heb de tutorial gevolgd van **Unity Learn 2D** om mij verder te verdiepen in de development van 2D-games. In deze tutorial wordt stapsgewijs uitgelegd hoe een 2D-game wordt opgebouwd.
  - Ik heb de documentatie van Unity uitgebreid doorgenomen;
- Dependency injection:
  - Ik heb **Zenject** gekozen als dependency injection framework zodat ik mijn project kan indelen in een collectie van 'loosely coupled' stukken code. Hiervoor heb ik eerst via github het nodige opzoekwerk verricht in de documentatie.
## Voorbereidingen 
- Installeren van **Unity** adhv PRI Unity Pdf
- Plugin voor **GitHub** toegevoegd aan Unity 
- **Unity Learn** documentatie
- **Zenject** documentatie
- Afbeeldingen zoeken:
  - Voor de sprites/images in dit project heb ik gebruik gemaakt van de gratis beschikbare png-bestanden van **Open Game Art**.
## Features
- Mogelijkheid om de tijdsduur per level in te stellen
- Mogelijkheid om te kiezen tussen verschillende karakters
- Weergeven van resterende tijd
- Weergeven van huidige score
- Weergeven van resterende levens aan de hand van hartjes
- Een karakter kan zich naar links of rechts navigeren
- Een karakter kan spring op en over obstakels
- Een karakter kan munten verzamelen die de score verhogen
- Een karakter kan hartjes verzamelen die de resterende levens van de speler verhogen (indien dit de maximum niet overschrijdt)
- Een karakter kan dozen vernietigen door omhoog tegen de onderkant van de doos te springen
- Een karakter kan een vijand vernietigen door te springen en op de bovenkant van de vijand te landen
- Een karakter verliest een leven wanneer een vijand frontaal tegen de speler loopt
- Game over wanneer de resterende tijd op 0 staat
- Game over wanneer de levens van de speler op 0 staan
- Game over wanneer een speler in een put valt
- Mogelijkheid om een spel te pauzeren (zonder dat de tijd verder loopt)
- Een pauzescherm en gameoverscherm met de mogelijkheid om terug te keren naar het hoofdmenu
- Een pauzescherm met de mogelijkheid om het spel te hervatten
- Een gameoverscherm met de mogelijkheid om het spel te herstarten

## Opbouw
- Ik heb ervoor gekozen om dit project op te splitsen in **controller** klassen en **service** klassen die gebruik maken van **interfaces**. Hierdoor wordt in de UI enkel gebruik gemaakt van controllers die achterliggend de nodige services raadplegen. 
- Om de services zo efficient mogelijk te kunnen gebruiken heb ik **dependency injection** toegevoegd via **Zenject**. Hiervoor heb ik een aparte klasse voorzien genaamd **GameInstaller** waarin zich een IOC container bevindt. Ook bevindt zich hierin een factory voor de dependency injection van een klasse waarin een game object wordt geinstantieerd, aangezien dit niet standaard in Zenject wordt gedaan.

## Links
- Unity Learn:
  - https://learn.unity.com/course/beginning-2d-game-development
- Unity Docs:
  - https://docs.unity3d.com/Manual/Unity2D.html
- Zenject:
  - https://github.com/modesttree/Zenject
- Open Game Art:
  - https://opengameart.org/content/platformer-art-deluxe
- Google:
  - https://google.com
- StackOverflow:
  - https://stackoverflow.com


 

