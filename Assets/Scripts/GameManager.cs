using io.newgrounds.components.Medal;
using io.newgrounds.objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : DestroyableSingleton<GameManager>
{
    public const string DumpLines = "<dump>";

    private enum LocationStates
    {
        NotStarted,
        GameStarted,
        OutsideGamestop,
        InsideGamestop,
        Chase,
        AtSchool,
    }

    private enum DogState
    {
        Ignored,
        Excited,
        Pissed,
        Humping,
        HumpingFace,
        RunningWithPack,
    }

    private enum CapeState
    {
        Down,
        Up
    }

    public Dictionary<string, string> Synonyms = new Dictionary<string, string>()
    {
        { "grab", "get" },
        { "take", "get" },
        { "pick up", "get" },
        { "store", "gamestop" },
        { "kick", "hit" },
        { "punch", "hit" },
        { "nudge", "push" },
        { "shove", "push" },
        { "go to", "goto" },
        { "walk to", "goto" },
        { "drive to", "goto" },
        { "new game", "game" },
        { "dotawatch", "game" },
        { "dota", "game" },
        { "overwatch", "game" },
        { "talk to", "talk" },
        { "flirt", "talk" },
        { "haggle", "talk" },
        { "impress", "talk" },
        { "seduce", "talk" },
        { "say hi", "talk" },
        { "look around", "look" },
        { "find", "look for" },
        { "cute girl", "girl" },
    };

    public io.newgrounds.core NgCore;

    public TextController MainScreen;
    public SpriteRenderer Shrug;
    public TextBox InputBox;

    [HideInInspector] private LocationStates locationState;
    [HideInInspector] private DogState dogState;
    [HideInInspector] private CapeState capeState;
    [HideInInspector] private bool talkingToGirl;
    [HideInInspector] private int fallingInLove;

    private List<string> output = new List<string>();
    private Dictionary<int, bool> medalList = new Dictionary<int, bool>();

    private void Start()
    {
        this.NgCore.onReady(NgioReady);
    }

    private void NgioReady()
    {
        getList getList = new getList();
        getList.callWith(
            this.NgCore,
            list => this.medalList = list.medals
                .Cast<medal>()
                .ToDictionary(m => m.id, m => m.unlocked));
    }

    public void ResetGame()
    {
        this.locationState = LocationStates.NotStarted;
        this.dogState = DogState.Ignored;
        this.capeState = CapeState.Down;
        this.talkingToGirl = false;
        this.fallingInLove = 0;
        SchoolLocation.Instance.Reset();
    }

    public void AcceptInput(string input)
    {
        if (this.output.Count > 0)
        {
            AppendOneLine();
            return;
        }

        input = SimplifyInput(this.Synonyms, input);
        Shrug.gameObject.SetActive(false);
        
        if (input.Length == 0) return;

        switch (this.locationState)
        {
            case LocationStates.NotStarted:
                if (input.Equals("be me"))
                {
                    this.locationState = LocationStates.GameStarted;
                    // MainScreen.BackgroundColor = new Color(245, 233, 225);
                    // MainScreen.Color = new Color(120 / 255f, 153 / 255f, 34 / 255f);
                    MainScreen.displayTxt = "";
                    output.Add("be me, 17");
                    output.Add("should go to school, but could go to gamestop instead");
                }
                else if (input.Equals("cheevos"))
                {
                    
                }
                else if (input.Equals("look"))
                {
                    output.Add("start the game, numbnuts");
                }

                break;
            case LocationStates.GameStarted:
                HandleGameStarted(input);
                break;
            case LocationStates.OutsideGamestop:
                HandleOutsideGamestop(input);
                break;
            case LocationStates.InsideGamestop:
                HandleInsideGamestop(input);
                break;
            case LocationStates.Chase:
                HandleChase(input);
                break;
            case LocationStates.AtSchool:
                SchoolLocation.Instance.HandleInput(output, input);
                break;
        }

        HandleAlways(input);

        if (input.Equals("fullscreen"))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
        else if (output.Count != 0)
        {
            AppendOneLine();
        }
        else
        {
            Shrug.gameObject.SetActive(true);
            Shrug.color = new Color(
                Random.value * .75f,
                Random.value * .75f,
                Random.value * .75f);
        }
    }

    private void HandleChase(string input)
    {
        if (this.dogState < DogState.RunningWithPack)
        {
            if (input.Equals("chase dog"))
            {
                output.Add("run after dog, nearly caught up");
                this.dogState = DogState.RunningWithPack;
            }
            else
            {
                output.Add("gotta get that dog, my money is in there!");
            }
        }
        else
        {
            /*if (input.Equals("get dog"))
            {
                //output.Add("tackle the dog, land with a fart");
                //output.Add("oh no, it's 4:30, time for my meeting");
            }
            else */
            if (input.Contains("get"))
            {
                output.Add("tear fanny pack away from dog");
                output.Add("spaghetti flies out at 650mph");
                output.Add("dog is impaled by brittle spaghetti");
                output.Add("I rocket, spinning into the air");
                output.Add("I reach a cumulonimbus and form a tornado");
                output.Add("spaghetti is thrown to far reaches of world, curing hunger for all");
                output.Add(GameManager.DumpLines);
                UnlockMedal(output, "be philanthropic", MedalTypes.BePhilanthropic);
                output.Add("(be me to play again)");
                this.ResetGame();
            }
        }
    }

    private void HandleInsideGamestop(string input)
    {
        if (!talkingToGirl)
        {
            if (input.Equals("look for game")
                || input.Equals("get game"))
            {
                output.Add("wtf, can't find game");
            }

            if (input.Equals("look for help")
                || input.Equals("ask for help")
                || input.Equals("look"))
            {
                output.Add("only employee is cute girl behind counter");
            }

            if (input.Equals("talk people"))
            {
                output.Add("rather talk to cute girl");
            }

            if (input.Equals("talk girl") || input.Equals("talk girl"))
            {
                output.Add("approach counter");
                output.Add("palms get sweaty");
                output.Add("ask cute girl for dotawatch");
                output.Add("she finds last copy in back room");
                output.Add("says it'll be $30");
                talkingToGirl = true;
            }
        }
        else
        {
            if (this.fallingInLove == 2)
            {
                output.Add("unzip my fannypack and pull out spaghetti");
                output.Add("a tsunami rushes forth from behind the counter");
                output.Add("it blasts me out of gamestop and carries on to destroy half the town");
                output.Add(GameManager.DumpLines);
                UnlockMedal(output, "be suave", MedalTypes.BeSuave);
                output.Add("(be me to play again)");
                this.ResetGame();
                return;
            }

            if (input.Contains("look"))
            {
                output.Add("looking closer at the girl, cute af");
                output.Add("I'm falling in love already");
            }

            if (input.Contains("talk"))
            {
                switch (this.fallingInLove)
                {
                    case 0:
                        output.Add("\"Well you know, shadow isn't actually related to sonic. He's just genetically altered\"");
                        output.Add("notimpressed.tiff");
                        output.Add("she must know that already");
                        output.Add("i hear dripping");
                        this.fallingInLove = 1;
                        break;
                    case 1:
                        output.Add("tip my fedora forward with best smoldering look");
                        output.Add("there is now a pool of water under the counter");
                        this.fallingInLove = 2;
                        break;
                }
            }
            else
            {
                if (input.Contains("money") || input.Contains("pay"))
                {
                    output.Add("unzip fanny pack for money");
                    output.Add("spaghetti falls out");
                    output.Add("everyone staring as there is spaghetti all over carpet");

                    if (this.capeState == CapeState.Up)
                    {
                        output.Add("quickly unlatch cape as it flutters over mess, covering it");
                        output.Add("phew.jpg");
                        output.Add("complete transaction");
                        output.Add("leave and enjoy new game");
                        output.Add("fucking gamestop");
                        output.Add(GameManager.DumpLines);
                        UnlockMedal(output, "be prepared", MedalTypes.BePrepared);
                        output.Add("(be me to play again)");
                        this.ResetGame();
                    }
                    else
                    {
                        output.Add("try to run out of store, but trip on cape");
                        output.Add("land in spaghetti, split head open");
                        output.Add("my blood looks like marinara, my brain matter meatballs");
                        output.Add("gamestop zombies feast for days");
                        output.Add(GameManager.DumpLines);
                        UnlockMedal(output, "be spaghetti", MedalTypes.BeSpaghetti);
                        output.Add("(be me to play again)");
                        this.ResetGame();
                    }
                }
            }
        }
    }

    public void HandleAlways(string input)
    {
        if (output.Count == 0 && input.Equals("be me"))
        {
            output.Add("still me");
        }

        if (input.Equals("lift cape"))
        {
            if (this.capeState == CapeState.Down)
            {
                output.Add("I lift my cape");
                this.capeState = CapeState.Up;
            }
            else
            {
                output.Add("my cape is still slowly floating down");
            }
        }

        if (output.Count == 0 && input.Equals("talk"))
        {
            output.Add("talk to whom?");
        }
    }

    private void HandleOutsideGamestop(string input)
    {
        if (this.dogState == DogState.Pissed)
        {
            output.Add("dog lunges for my jugular");
            output.Add("lightning reflex dodge, but");
            output.Add("dog rips off my fanny pack and bolts");
            this.locationState = LocationStates.Chase;
            return;
        }
        else if (this.dogState == DogState.HumpingFace)
        {
            output.Add("dog cums in my mouth, it tastes like...");
            output.Add("spaghetti?!");
            output.Add("open my eyes, at dinner with fam");
            output.Add("i vomit over the table");
            output.Add("grounded for a week");
            output.Add(GameManager.DumpLines);
            output.Add("(be me to play again)");
            UnlockMedal(output, "be dreaming", MedalTypes.BeDreaming);
            this.ResetGame();
            return;
        }

        if (input.Equals("look at dog"))
        {
            switch (this.dogState)
            {
                case DogState.Ignored:
                    output.Add("some kinda lab, pretty cute");
                    break;
                case DogState.Excited:
                    output.Add("he def wants more pets");
                    break;
                case DogState.Humping:
                    output.Add("stare down at him, he shows no signs of stopping");
                    break;
            }
        }

        if (input.Equals("pet dog"))
        {
            switch (this.dogState)
            {
                case DogState.Ignored:
                    output.Add("pet dog, he gets really excited");
                    this.dogState = DogState.Excited;
                    break;
                case DogState.Excited:
                    output.Add("pet dog more, he starts humping my leg");
                    this.dogState = DogState.Humping;
                    break;
                case DogState.Humping:
                    output.Add("look down to continue petting, realize am dog");
                    output.Add("mount gamestop dog");
                    output.Add("\"how do you like it?\", I howl");
                    output.Add("oh shit animal control");
                    output.Add("run away to join wolves");
                    output.Add("grrr gamestop");
                    output.Add(GameManager.DumpLines);
                    UnlockMedal(output, "be dog", MedalTypes.BeDog);
                    output.Add("(be me to play again)");
                    this.ResetGame();
                    break;
            }
        }

        if (input.Equals("push dog"))
        {
            switch (this.dogState)
            {
                case DogState.Ignored:
                case DogState.Excited:
                    output.Add("nudge dog, no response");
                    break;
                case DogState.Humping:
                    output.Add("oh shit, can't push him off, humping like crazy");
                    break;
            }
        }

        if (input.Equals("hit dog"))
        {
            switch (this.capeState)
            {
                case CapeState.Down:
                    output.Add("wind up to kick the dog, he dodges");
                    output.Add("trip on my cape, fall down");
                    output.Add("dog starts humping my face");
                    this.dogState = DogState.HumpingFace;
                    break;
                case CapeState.Up:
                    output.Add("kick the dog, he yelps but comes back around");
                    output.Add("starts growling");
                    this.dogState = DogState.Pissed;
                    break;
            }
        }

        if (input.Equals("inside")
            || input.Equals("goto gamestop")
            || input.Equals("go inside")
            || input.Equals("open door")
            || input.Equals("ignore dog"))
        {
            if (dogState == DogState.Humping)
            {
                output.Add("want game, but gotta get this dog off me first");
            }
            else
            {
                output.Add("ignore dog, go inside");
                this.locationState = LocationStates.InsideGamestop;
            }
        }

        if (input.Contains("game") || input.Equals("look"))
        {
            output.Add("could go inside for game, but this dog looks like it needs pets");
        }
    }

    private void HandleGameStarted(string input)
    {
        if (input.Contains("game"))
        {
            output.Add("went to gamestop to buy a copy of dotawatch");
            output.Add("be outside, cute dog with \"do not pet\" sign");
            this.locationState = LocationStates.OutsideGamestop;
        }
        else if (input.Contains("school"))
        {
            // playing fav ntr eroge when, suddenly very hungry
            this.locationState = LocationStates.AtSchool;
            output.Add("at school, sitting next to cute girl");
            output.Add("I hatch a plan to steal her pencil for a chance at interaction");
            output.Add("but beta af, so probably just eat cheetos");
        }
        else
        {
            output.Add("gamestop or school, tough choice");
        }
    }

    public static string SimplifyInput(Dictionary<string, string> synonyms, string input)
    {
        input = input.Replace(" a ", "").Replace(" the ", "");

        string output = input;
        do
        {
            input = output;
            foreach (var kvp in synonyms)
            {
                output = output.Replace(kvp.Key, kvp.Value);
            }

        } while (!output.Equals(input));

        return output;
    }

    public void UnlockMedal(List<string> output, string name, MedalTypes medalId)
    {
        bool unlocked;
        if (this.medalList == null)
        {
            output.Add("Could not unlock " + name + " medal");
            this.NgioReady();
        }
        else if (!this.medalList.TryGetValue((int)medalId, out unlocked))
        {
            output.Add("Programmer fucked up " + name + " medal");
            return;
        }
        else if (!unlocked)
        {
            output.Add("Unlocked the \"" + name + "\" medal");
            this.medalList[(int)medalId] = true;
            unlock medal_unlock = new unlock();
            medal_unlock.id = (int)medalId;
            medal_unlock.callWith(NgCore);
        }
        else
        {
            output.Add("Already have " + name + " medal");
        }

        output.Add("Medals: " + this.medalList.Count(k => k.Value) + "/" + this.medalList.Count);
    }
    
    private void AppendOneLine()
    {
        StringBuilder builder = new StringBuilder(MainScreen.displayTxt);
        builder.Replace("[000000]", "").Replace("(more)\n", "");
        builder.Append("[000000]");
        string newLine = output[0];
        output.RemoveAt(0);
        
        if (newLine.Equals(DumpLines))
        {
            while (output.Count > 0)
            {
                newLine = output[0];
                output.RemoveAt(0);
                builder.Append(">" + newLine + "\n");
            }
        }
        else
        {
            builder.Append(">" + newLine + "\n");
        }

        MainScreen.displayTxt = builder.ToString();
        this.InputBox.AnyKey = output.Count > 0;
    }
}
