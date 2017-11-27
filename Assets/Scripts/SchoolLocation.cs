using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SchoolLocation : DestroyableSingleton<SchoolLocation>
{
    private Dictionary<string, string> Synonyms = new Dictionary<string, string>()
    {
        { "clean", "wipe" },
        { "smear", "wipe" },
        { "lick", "eat" },
        { "taste", "eat" },
        { "steal", "get" },
        { "cheetos", "chips" },
        { "have sex with", "fuck" },
        { "bone", "fuck" },
        { "bang", "fuck" },
        { "offer", "give" }
    };

    public enum Locations
    {
        Start,
        Pencil,
        Cheetos
    }

    public enum PencilState
    {
        NotStolen,
        Stolen,
        Shitty,
        Shitty2,
        Diamonds,
    }

    public GameManager GameManager;

    public Locations locationState = Locations.Start;
    public PencilState pencilState = PencilState.NotStolen;

    public void Reset()
    {
        this.locationState = Locations.Start;
        this.pencilState = PencilState.NotStolen;
    }

    public void HandleInput(List<string> output, string input)
    {
        input = GameManager.SimplifyInput(this.Synonyms, input);

        switch (this.locationState)
        {
            case Locations.Start:
                HandleStart(output, input);
                break;
            case Locations.Pencil:
                HandlePencil(output, input);
                break;
            case Locations.Cheetos:
                HandleCheetos(output, input);
                break;
        }
    }

    private void HandleCheetos(List<string> output, string input)
    {
        if (input.Equals("fuck chips"))
        {
            if (this.pencilState == PencilState.Diamonds)
            {
                output.Add("like I really fucking love cheetos");
                output.Add("I whip my dick out and pulverize the bright red chips into powder");
                output.Add("but the spices begin to burn my cock");
                output.Add("it grows painfully red and irritated");
                output.Add("suddenly hohoho.wav");
                output.Add("Santas busts the fuck in like the kool-aid man");
                output.Add("he surveys the room, and points directly at me");
                output.Add("i stand, with my bright red erection and salute him");
                output.Add("he rams a pole up my ass and mounts me on his sleigh, guiding him on his deliveries");
                output.Add("you're welcome");
                output.Add("");
                GameManager.Instance.UnlockMedal(output, "be rudolph", MedalTypes.BeRudolph);
                output.Add("(be me to play again)");
                GameManager.Instance.ResetGame();
                return;
            }
            else
            {
                output.Add("would fuck cheetos, I love them so much, but not hard right now");
            }
        }

        if (input.Contains("give chips"))
        {
            output.Add("I offer a cheeto to the cute girl");
            output.Add("she smacks it out of my hand in disgust");
            output.Add("the combined anguish of rejection and losing a cheeto causes me to burst into treats");
            output.Add("The girl picks up a piece and says");
            output.Add("\"now das what'm talkin' about, sugah!\"");
            output.Add("");
            GameManager.Instance.UnlockMedal(output, "be treats", MedalTypes.BeTreats);
            output.Add("(be me to play again)");
            GameManager.Instance.ResetGame();
        }
    }

    private void HandlePencil(List<string> output, string input)
    {
        switch (this.pencilState)
        {
            case PencilState.Stolen:
                if (input.Contains("look"))
                {
                    output.Add("she looks like she wants a pencil");
                }

                if (input.Contains("pencil"))
                {
                    output.Add("Pull out her pencil, it's covered in pocket chocolate");
                    output.Add("I stammer, \"I-it's not poop\"");
                    this.pencilState = PencilState.Shitty;
                }
                break;
            case PencilState.Shitty:
                if (input.Contains("wipe"))
                {
                    output.Add("wipe the pencil. now I'm covered in chocolate too.");
                    output.Add("shes screaming now, everyone's looking");
                    output.Add("\"It's not poop!\", I yell");
                    this.pencilState = PencilState.Shitty2;
                }
                else if (input.Contains("eat"))
                {
                    output.Add("eat some chocolate. \"see?\"");
                    output.Add("\"oh, okay\", she says, and reaches for the pencil");
                    output.Add("She slips and grabs my crotch instead");
                    output.Add("instadiamonds.jpg");
                    output.Add("she apologizes and takes her pencil");
                    output.Add("class continues");
                    this.pencilState = PencilState.Diamonds;
                    this.locationState = Locations.Start;
                }
                break;
            case PencilState.Shitty2:
                output.Add("spaghetti erupts from my pockets, covered in chocolate");
                output.Add("students start hopping on their desks");
                output.Add("hooting and screeching, flinging pocket chocolate at each other");
                output.Add("kids start supplementing chocolate with real shit");
                output.Add("the teacher sits calmly chewing a banana as shitty chocolate spaghetti flies past");
                // TODO: output.Add("");
                output.Add("");
                GameManager.Instance.UnlockMedal(output, "be not poop", MedalTypes.BeNotPoop);
                output.Add("(be me to play again)");
                GameManager.Instance.ResetGame();
                break;
        }
    }

    private void HandleStart(List<string> output, string input)
    {
        if (input.Equals("look"))
        {
            output.Add("sitting in class, next to cute girl");
            if (this.pencilState == PencilState.NotStolen)
            {
                output.Add("I can a pencil in her desk, and I have cheetos in my own.");
                output.Add("steal pencil, eat cheetos");
            }
            else
            {
                output.Add("I have cheetos in my desk.");
            }
        }

        if (input.Equals("get pencil"))
        {
            if (this.pencilState == PencilState.NotStolen)
            {
                output.Add("quickly steal pencil so she'll need to talk to me");
                output.Add("stuff it in pocket for safe keeping");
                output.Add("later, she asks to borrow pencil");
                this.locationState = Locations.Pencil;
                this.pencilState = PencilState.Stolen;
            }
            else
            {
                output.Add("can't steal her pencil again");
            }
        }
        else if (input.Equals("eat chips"))
        {
            output.Add("start eating flaming hot cheetos");
            output.Add("I love this shit");
            this.locationState = Locations.Cheetos;
        }

    }
}
