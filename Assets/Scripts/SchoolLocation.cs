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
        Cheetos,
        Talking
    }

    public enum PencilState
    {
        NotStolen,
        Stolen,
        Shitty,
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
            case Locations.Talking:
                HandleTalking(output, input);
                break;
        }
    }

    private void HandleTalking(List<string> output, string input)
    {
        
    }

    private void HandleCheetos(List<string> output, string input)
    {
        if (input.Contains("look"))
        {
            if (this.pencilState == PencilState.Diamonds)
            {
                output.Add("Man, this cheetos look so good I could fuck them");
            }
            else
            {
                output.Add("Bag still half full of cheetos");
            }
        }
        else if (input.Equals("eat chips"))
        {
            output.Add("Crunch, crunch");
            output.Add("Cute girl looks over, \"That's annoying, could you stop?\"");
            if (this.pencilState == PencilState.Diamonds)
            {
                output.Add("Instantly cum from girl talking to me for a second time today");
                output.Add("Suddenly nurse busts in and bangs the ceremonial penis inspection day gong");
                output.Add("I try to hide my cheetos, but the crinkling makes me a target");
                output.Add("I try to run, but the nurse grabs me and suplexes me");
                output.Add("Cheetos shit gushes from my anus and fills my pants upon impact");
                output.Add("She unzips my pants to find cheeto cum shit balls");
                output.Add("I then vomit flaming hot cheetos which ignite upon touching the air");
                output.Add("but I rolled a critical miss, so the flames back up and I explode");
                output.Add("fucking penis inpection day");
                output.Add(GameManager.DumpLines);
                GameManager.Instance.UnlockMedal(output, "be inspected", MedalTypes.BeInspected);
                output.Add("(be me to play again)");
                GameManager.Instance.ResetGame();
                return;
            }
        }
        else if (input.Equals("fuck chips"))
        {
            if (this.pencilState == PencilState.Diamonds)
            {
                output.Add("I whip my dick out and pulverize the bright red chips into powder");
                output.Add("but the spices begin to burn my cock");
                output.Add("it grows painfully red and irritated");
                output.Add("suddenly hohoho.wav");
                output.Add("Santas busts the fuck in like the kool-aid man");
                output.Add("he surveys the room, and points directly at me");
                output.Add("i stand, with my bright red erection and salute him");
                output.Add("he rams a pole up my ass and mounts me on his sleigh, guiding him on his deliveries");
                output.Add("you're welcome");
                output.Add(GameManager.DumpLines);
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
        else if (input.Contains("give chips"))
        {
            output.Add("I offer a cheeto to the cute girl");
            output.Add("she smacks it out of my hand in disgust");
            output.Add("the combined anguish of rejection and losing a cheeto causes me to burst into treats");
            output.Add("The girl picks up a piece and says");
            output.Add("\"now das what'm talkin' about, sugah!\"");
            output.Add(GameManager.DumpLines);
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
                else if (
                    input.Equals("get pencil")
                    || input.Equals("give pencil"))
                {
                    output.Add("Pull out her pencil, it's covered in pocket chocolate");
                    output.Add("She sees it. Mortified, I stammer, \"I-it's not poop\"");
                    output.Add("Think: fuck, I gotta get this chocolate off somehow");
                    this.pencilState = PencilState.Shitty;
                }
                else if (input.Contains("talk"))
                {
                    output.Add("Hands clam up");
                    output.Add("O-oh, yeah. J-j-just a sec.");
                    output.Add("Shove hand in pocket");
                    output.Add("Nothing");
                    output.Add("Wtfwtf, growing more frantic as I check each pocket once, then twice");
                    output.Add("Shove hands in pockets so hard and deep Mrs. TeacherLady collapses in orgasm");
                    output.Add("Tear open backpack with such force, blackhole opens");
                    output.Add("Time stretches to infinity as as I'm pulled into my own undoing");
                    output.Add("My last sight before my eyes are atomized is a pencil under my desk");
                    output.Add(GameManager.DumpLines);
                    GameManager.Instance.UnlockMedal(output, "be atomized", MedalTypes.BeAtomized);
                    output.Add("inb4 that's not how blackholes are formed");
                    output.Add("(be me to play again)");
                    GameManager.Instance.ResetGame();
                }
                break;
            case PencilState.Shitty:
                if (input.Contains("look"))
                {
                    output.Add("be panicked, holding a pencil covered in chocolate.");
                    output.Add("Fuck, I gotta wipe this up or like eat it or something.");
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
                else if (input.Contains("wipe"))
                {
                    output.Add("wipe the pencil. now I'm covered in chocolate too.");
                    output.Add("shes screaming now, everyone's looking");
                    output.Add("\"It's not poop!\", I yell");
                    output.Add("spaghetti erupts from my pockets, covered in chocolate");
                    output.Add("students start hopping on their desks");
                    output.Add("hooting and screeching, flinging pocket chocolate at each other");
                    output.Add("kids start supplementing chocolate with real shit");
                    output.Add("the teacher sits calmly chewing a banana as shitty chocolate spaghetti flies past");
                    output.Add("My vision fades as I succumb to \"It's not poop\"");
                    output.Add(GameManager.DumpLines);
                    GameManager.Instance.UnlockMedal(output, "be not poop", MedalTypes.BeNotPoop);
                    output.Add("(be me to play again)");
                    GameManager.Instance.ResetGame();
                }
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
                output.Add("I can see a pencil in her desk, and I have cheetos in my own.");
            }
            else
            {
                output.Add("I have cheetos in my desk and a hard-on in my pants.");
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
        else if (input.Equals("talk girl"))
        {
            output.Add("I stammer out something insightful towards the girl");
            output.Add("\"Vriska isn't really fat, Andrew Hussie is just a troll.\"");
            output.Add("She turns towards me, \"Are you talking to me?\"");
            this.locationState = Locations.Talking;
        }
        else if (input.Equals("fuck girl"))
        {
            output.Add("She's a 2/10, so obv would fuck, but I should talk to her or something first.");
        }
    }
}
