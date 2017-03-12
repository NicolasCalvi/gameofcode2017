/// <summary>
/// Liste des messages supportés
/// </summary>
public enum GameMessageTypes : int
{
    Unknow = 0,
    ClientServerDetail = 1,
    SendObjectInMailbox = 2
}

/// <summary>
/// Liste des objets transitants par la boite aux lettres
/// </summary>
public enum MailboxItems : int
{
    None = 0,
    Menu = 1,
    Screeen = 2,
    Key1 = 3,
    UsbKey = 4,
    Key2 = 5,
    DoorHandle = 6
}