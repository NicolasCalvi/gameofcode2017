TEAM ONEPOINT
https://www.groupeonepoint.com/

+ Equipe

Nicolas Calvi / Consultant Innovation, Expert NUI / Microsoft MVP Windows Development

Pierrick Filiatrault / Consultant Sénior

Anaïs Neveux / Stagiaire Réalité Mixte

Simon Besga / Stagiaire Réalité Virtuelle

-------------------------------------------------------------------
PROJET : Virtual Cage

+ Concept

Le but est de démontré une interaction entre deux mondes a perception distincte, l'une à partir d'un Hololens (Réalité Mixte) et l'autre d'un Gear VR (Réalité Virtuelle). L'histoire est celle d'un personne enfermée dans une monde virtuel qui ne vois que sont monde et une autre personne qui voit la personne enfermée dans la réalité enfermée dans une cage. Le but étant de la libérer.

C'est donc un escape game, ou les deux personnes peuvent se parler mais seule celle avec l'Hololens peux voir l'autre. Il devrons donc se décrire leurs mondes pour collaborer. Ils auront a disposition une boite au lettre dimensionnelle pour se passer des objets. Pour pouvoir avancée ils doivent résoudre des énigmes.

+ Déroulé de l'expérience

- Début 
GVR : Indication pour dire que le mec est emprisonné, mais ton ami peut le libérer. Vous pouvez vous parler, mais seul ton ami peut te voir. Toi tu es dans un monde alternatif.
HL : Ton ami est prisonnier il faut le faire sortir, vous pouvez discuter entre vous, mais seul toi peut le voir.
Global : Il faut collaborer ensemble pour le faire sortir.

- 1ère étape
Une boite au lettre avec une lumière permet de se transférer des objets.
Il faut se transférer une premier objet. Il existe coté GVR et doit arriver coté HL. (Objet boule de couleur).
Une fois transféré, cela active les actions coté HL. 
Affiche une boule qui suit la personne et qui affiche une menu quand elle dit menu. La sélection des entrée se fait en vocal
Bonus => Si on s'approche d'un objet elle clignote.

- 2ème étape :
HL => Choisir le premier menu, ce qui affiche un filtre et fait apparaitre un pad qu'il faut transférer dans le GVR.
GVR => Réception pad, affichage énigme textuelle. Après résolution, une clé tombe, qu'il faut transférer dans le monde HL.
HL => Réception de la clé et déblocage du second menu.

- 3ème étape :
HL => Choisir le second menu, ce qui affiche un filtre et fait apparaitre une énigme a résoudre pour débloquer une clé USB.
Transmission de la clé USB coté GVR.
GVR => Réception de la clé et activation de la seconde énigme textuelle. Cela affiche une porte en hologramme, A réussite, drop une clé et la rend opaque.
qu'il faut transférer dans le monde HL.
HL => Réception de la clé et déblocage du dernier menu.

- 4ème étape :
HL => Choisir le dernier menu, ce qui affiche un filtre et fait apparaitre une énigme a résoudre pour débloquer une clé poignée.
Transmission de la pognée coté GVR.
GVR => Réception de la poignée, la mettre sur la porte et sortir, fin du jeu.

-------------------------------------------------------------------
TECHNOLOGIES

+ Architecture

Il y a deux applications, une coté Gear VR et l'autre coté Hololens.
Elles communiquent via un pont réseau.

+ Framework

Hololens => Application en Unity 3D 5.5.2 (Target UWP) avec Holotoolkit, NGUI, Playmaker, ShaderForge et une librairie a nous pour la gestion du Gaze.

Gear VR => Application en Unity 3D 5.5.2 (Target Android) avec OVR, NGUI, Playmaker, ShaderForge et une librairie a nous pour la gestion du Gaze.

Server => .Net WCF 4.6.2 avec un endpoint Http REST.

-------------------------------------------------------------------
INFORMATIONS

+ Problématique de développent

Nous avons eu plusieurs soucis, le premier est lié au réseau. Nous avons fait une interface Socket, mais les objets Mono (TcpListner, TcpClient, Socket) ne sont pas compatible avec le framework UWP Windows 10 d'hololens. Nous avons essayez avec le NetworkView de Unity, mais pareil, non compatible UWP. La seul solution viable est un code C++, mais trop long a mettre en place. Nous somme donc partie sur un serveur WEB qui exposte une api REST très simple, et les applications pool cette API. Cela fonctionne c'est le principal.

+ Problématique mise à jour du Holotoolkit

Le Holotoolkit a été mis à jour avec une refonte des Gestures, cette évolution n'est pas encore stable et nous avons rencontrer des problèmes pour récupérer la gesture d'Air Tap. Après investigation on a fini par trouver un code compatible avec le Air Tap avec les Api basses Unity.

+ Temps

Suites aux divers problématiques, nos n'avons pas eu le temps d'implémenter les éngimes, nous nous sommes donc concentré sur l'enchainement des objets via la boite au lettre pour dérouler le scénario.
