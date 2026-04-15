using Trees;
using Graphs;
public static class Program
{
    static UndirectedGraph<string> europe = new();

    static Vertex<string> albania = new Vertex<string>("Albania ");
    static Vertex<string> andorra = new Vertex<string>("Andorra");
    static Vertex<string> austria = new Vertex<string>("Austria");
    static Vertex<string> belarus = new Vertex<string>("Belarus");
    static Vertex<string> belgium = new Vertex<string>("Belgium");
    static Vertex<string> bosnia = new Vertex<string>("Bosnia and Herzegovina");
    static Vertex<string> bulgaria = new Vertex<string>("Bulgaria");
    static Vertex<string> croatia = new Vertex<string>("Croatia");
    static Vertex<string> cyprus = new Vertex<string>("Cyprus");
    static Vertex<string> czechia = new Vertex<string>("Czechia");
    static Vertex<string> denmark = new Vertex<string>("Denmark");
    static Vertex<string> estonia = new Vertex<string>("Estonia");
    static Vertex<string> finland = new Vertex<string>("Finland");
    static Vertex<string> france = new Vertex<string>("France");
    static Vertex<string> georgia = new Vertex<string>("Georgia");
    static Vertex<string> germany = new Vertex<string>("Germany");
    static Vertex<string> greece = new Vertex<string>("Greece");
    static Vertex<string> hungary = new Vertex<string>("Hungary");
    static Vertex<string> iceland = new Vertex<string>("Iceland");
    static Vertex<string> ireland = new Vertex<string>("Ireland");
    static Vertex<string> italy = new Vertex<string>("Italy");
    static Vertex<string> kosovo = new Vertex<string>("Kosovo");
    static Vertex<string> latvia = new Vertex<string>("Latvia");
    static Vertex<string> liechtenstein = new Vertex<string>("Liechtenstein");
    static Vertex<string> lithuania = new Vertex<string>("Lithuania");
    static Vertex<string> luxembourg = new Vertex<string>("Luxembourg");
    static Vertex<string> malta = new Vertex<string>("Malta");
    static Vertex<string> moldova = new Vertex<string>("Moldova");
    static Vertex<string> monaco = new Vertex<string>("Monaco");
    static Vertex<string> montenegro = new Vertex<string>("Montenegro");
    static Vertex<string> netherlands = new Vertex<string>("Netherlands");
    static Vertex<string> macedonia = new Vertex<string>("North Macedonia");
    static Vertex<string> norway = new Vertex<string>("Norway");
    static Vertex<string> poland = new Vertex<string>("Poland");
    static Vertex<string> portugal = new Vertex<string>("Portugal");
    static Vertex<string> romania = new Vertex<string>("Romania");
    static Vertex<string> russia = new Vertex<string>("Russia");
    static Vertex<string> sanmarino = new Vertex<string>("San Marino");
    static Vertex<string> serbia = new Vertex<string>("Serbia");
    static Vertex<string> slovakia = new Vertex<string>("Slovakia");
    static Vertex<string> slovenia = new Vertex<string>("Slovenia");
    static Vertex<string> spain = new Vertex<string>("Spain");
    static Vertex<string> sweden = new Vertex<string>("Sweden");
    static Vertex<string> switzerland = new Vertex<string>("Switzerland");
    static Vertex<string> turkey = new Vertex<string>("Turkey");
    static Vertex<string> ukraine = new Vertex<string>("Ukraine");
    static Vertex<string> uk = new Vertex<string>("United Kingdom");
    static Vertex<string> vatican = new Vertex<string>("Vatican");

    static int printCounter = 1;

    static void Main()
    {
        InitializeCountries();
        BuildBorders();
        ExportToHtml();

        PrintHeader("BFS");
        europe.BFS(Print, czechia);

        PrintHeader("DFS");
        printCounter = 1;
        europe.DFS(Print, czechia);

        PrintHeader("Connections");
        PrettyConnect(albania, czechia);
        PrettyConnect(ireland, uk);
        PrettyConnect(italy, uk);

        PrintHeader("Components");
        Console.WriteLine(europe.CountComponents());
    }

    static void PrintHeader(string s)
    {
        Console.WriteLine("--------- " + s + " ---------");
    }

    static void Print(Vertex<string> vertex)
    {
        Console.WriteLine(printCounter + ". " + vertex.Value);
        printCounter++;
    }

    static void PrettyConnect(Vertex<string> start, Vertex<string> end)
    {
        if (europe.AreConnected(start, end))
            Console.WriteLine(start.Value + " and " + end.Value + " ARE connected");
        else
            Console.WriteLine(start.Value + " and " + end.Value + " ARE NOT connected");
    }

    static void InitializeCountries()
    {
        europe.Insert(albania);
        europe.Insert(andorra);
        europe.Insert(austria);
        europe.Insert(belarus);
        europe.Insert(belgium);
        europe.Insert(bosnia);
        europe.Insert(bulgaria);
        europe.Insert(croatia);
        europe.Insert(cyprus);
        europe.Insert(czechia);
        europe.Insert(denmark);
        europe.Insert(estonia);
        europe.Insert(finland);
        europe.Insert(france);
        europe.Insert(georgia);
        europe.Insert(germany);
        europe.Insert(greece);
        europe.Insert(hungary);
        europe.Insert(iceland);
        europe.Insert(ireland);
        europe.Insert(italy);
        europe.Insert(kosovo);
        europe.Insert(latvia);
        europe.Insert(liechtenstein);
        europe.Insert(lithuania);
        europe.Insert(luxembourg);
        europe.Insert(malta);
        europe.Insert(moldova);
        europe.Insert(monaco);
        europe.Insert(montenegro);
        europe.Insert(netherlands);
        europe.Insert(macedonia);
        europe.Insert(norway);
        europe.Insert(poland);
        europe.Insert(portugal);
        europe.Insert(romania);
        europe.Insert(russia);
        europe.Insert(sanmarino);
        europe.Insert(serbia);
        europe.Insert(slovakia);
        europe.Insert(slovenia);
        europe.Insert(spain);
        europe.Insert(sweden);
        europe.Insert(switzerland);
        europe.Insert(turkey);
        europe.Insert(ukraine);
        europe.Insert(uk);
        europe.Insert(vatican);
    }
    static void BuildBorders()
    {
        // --- YOUR START ---
        LinkAll(czechia, [(poland, 517), (slovakia, 290), (austria, 252), (germany, 280)]);
        LinkAll(poland, [(russia, 1150), (lithuania, 395), (belarus, 475), (ukraine, 690), (slovakia, 530), (germany, 518)]);
        LinkAll(russia, [(georgia, 1650), (ukraine, 755), (belarus, 675), (latvia, 840), (estonia, 860), (finland, 890)]);
        LinkAll(ukraine, [(moldova, 400), (romania, 740), (hungary, 890), (slovakia, 1000), (belarus, 430)]);
        LinkAll(moldova, [(romania, 358)]);
        LinkAll(romania, [(bulgaria, 295), (serbia, 450), (hungary, 640)]);
        LinkAll(bulgaria, [(turkey, 800), (greece, 520), (macedonia, 175), (serbia, 325)]);

        // --- WESTERN HUB ---
        LinkAll(france, [(belgium, 262), (luxembourg, 287), (germany, 878), (switzerland, 435), (italy, 1105), (spain, 1052), (andorra, 709), (monaco, 689)]);
        LinkAll(belgium, [(netherlands, 173), (luxembourg, 187), (germany, 602)]);
        LinkAll(luxembourg, [(germany, 603)]);
        LinkAll(netherlands, [(germany, 577)]);
        LinkAll(spain, [(portugal, 502), (andorra, 492)]);

        // --- ALPS & CENTRAL ---
        LinkAll(switzerland, [(germany, 750), (austria, 601), (italy, 692), (liechtenstein, 158)]);
        LinkAll(austria, [(slovakia, 55), (hungary, 214), (slovenia, 278), (italy, 540), (liechtenstein, 160)]);
        LinkAll(slovakia, [(hungary, 161)]);

        // --- BALKANS ---
        LinkAll(slovenia, [(croatia, 117), (hungary, 383), (italy, 441)]);
        LinkAll(croatia, [(bosnia, 288), (serbia, 330), (montenegro, 452), (hungary, 301)]);
        LinkAll(serbia, [(hungary, 316), (montenegro, 282), (bosnia, 209), (macedonia, 322), (kosovo, 251)]);
        LinkAll(albania, [(greece, 482), (macedonia, 161), (montenegro, 131), (kosovo, 186)]);
        LinkAll(greece, [(macedonia, 412), (turkey, 811)]);
        LinkAll(kosovo, [(macedonia, 78), (montenegro, 150)]);
        LinkAll(bosnia, [(montenegro, 175)]);

        // --- NORTH & BALTIC ---
        LinkAll(denmark, [(germany, 356)]);
        LinkAll(sweden, [(norway, 416), (finland, 396)]);
        LinkAll(finland, [(estonia, 82), (norway, 780)]);
        LinkAll(estonia, [(latvia, 279)]);
        LinkAll(latvia, [(lithuania, 262), (belarus, 401)]);
        LinkAll(lithuania, [(belarus, 172)]);

        // --- THE MINI-STATES & ISLANDS ---
        LinkAll(italy, [(vatican, 4), (sanmarino, 226)]);
        LinkAll(uk, [(ireland, 463)]);
        //LinkAll(iceland, [(uk, 1890), (norway, 1490)]);
        //LinkAll(malta, [(italy, 680)]);
        LinkAll(cyprus, [(turkey, 520)]);
    }

    static void LinkAll(Vertex<string> source, (Vertex<string>, int)[] targets)
    {
        foreach (var (target, weight) in targets)
        {
            europe.Link(source, target, weight);
        }
    }
    static void ExportToHtml()
    {
        // Map each vertex to a numeric id for vis.js
        var vertexList = europe.Vertices.ToList();
        var vertexIndex = vertexList
            .Select((v, i) => (v, i))
            .ToDictionary(x => x.v, x => x.i);

        var nodesJson = System.Text.Json.JsonSerializer.Serialize(
            vertexList.Select(v => new
            {
                id = vertexIndex[v],
                label = v.Value.Trim()
            })
        );

        var edgesJson = System.Text.Json.JsonSerializer.Serialize(
            europe.Edges.Select(e => new
            {
                from = vertexIndex[e.Source],
                to = vertexIndex[e.Target],
                label = e.Weight + " km"
            })
        );

        string html = $$"""
    <!DOCTYPE html>
    <html>
    <head>
      <meta charset="utf-8"/>
      <title>Europe Graph</title>
      <script src="https://unpkg.com/vis-network/standalone/umd/vis-network.min.js"></script>
      <style>
        body { margin: 0; background: #1a1a2e; }
        #graph { width: 100vw; height: 100vh; }
      </style>
    </head>
    <body>
      <div id="graph"></div>
      <script>
        const nodes = new vis.DataSet({{nodesJson}});
        const edges = new vis.DataSet({{edgesJson}});

        const options = {
          nodes: {
            shape: "dot",
            size: 12,
            color: { background: "#e94560", border: "#fff" },
            font: { color: "#ffffff", size: 13 }
          },
          edges: {
            color: { color: "#888888" },
            font: { color: "#e94560", size: 13, align: "middle" },
            smooth: { type: "continuous" }
          },
          physics: {
            solver: "forceAtlas2Based",
            stabilization: { iterations: 200 }
          }
        };

        new vis.Network(
          document.getElementById("graph"),
          { nodes, edges },
          options
        );
      </script>
    </body>
    </html>
    """;

        string path = Path.Combine(Path.GetTempPath(), "europe_graph.html");
        File.WriteAllText(path, html);

        // Open in default browser — works on Windows, macOS, Linux
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(path)
        {
            UseShellExecute = true
        });

        Console.WriteLine($"Graph opened in browser. File saved to: {path}");
    }
}