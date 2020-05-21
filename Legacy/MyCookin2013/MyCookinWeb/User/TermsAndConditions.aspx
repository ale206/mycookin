<%@ Page Title="" Language="C#" MasterPageFile="~/Styles/SiteStyle/Public.Master"
    AutoEventWireup="true" CodeBehind="TermsAndConditions.aspx.cs" Inherits="MyCookinWeb.User.TermsAndConditions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="Stylesheet" href="/Styles/PageStyle/TermsAndConditions.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <script type="text/javascript" src="/Js/Scrollbar/jquery.mCustomScrollbar.concat.min.js"></script>
    <script type="text/javascript" src="/Js/Pages/TermsAndConditions.js"></script>
    <asp:Panel ID="pnlConditions" CssClass="pnlConditions" ToolTip="Termini e Condizioni"
        ClientIDMode="Static" runat="server">
        <p class="titleRow">
            <asp:Label ID="lblTitle" CssClass="lblTitle" runat="server" Text="Termini e Condizioni"></asp:Label>
        </p>
        <asp:Panel runat="server" ID="pnlPrivacyText" ClientIDMode="Static" CssClass="content">
            Art. 1 – Oggetto e ambito di applicazione
            <ul>
                <li>1.1 Le presenti condizioni generali regolano tutti i servizi, nessuno escluso, offerti
                    tramite il portale all’url http://www.mycookin.com, di seguito denominato “il portale”
                    o “il sito”, di proprietà e gestito da MyCookin Ltd di seguito denominato “gestore
                    del portale”.</li>
                <li>1.2 Il gestore del portale si riserva espressamente il diritto di prevedere, a
                    fronte dell’offerta di servizi specifici, condizioni particolari per gli stessi
                    in deroga a quanto previsto nelle presenti Condizioni Generali.</li>
                <li>1.3 L’utilizzo del portale da parte dell’utente comporta accettazione incondizionata
                    delle presenti Condizioni Generali. Per usufruire di servizi specifici quali l’accesso
                    alla community l’utente dovrà preventivamente accettare le condizioni specifiche
                    dettate dall’art. 6 delle presenti Condizioni Generali.</li>
            </ul>
            <br />
            <br />
            Art. 2 – Oggetto del servizio e funzionalità offerte dal sito
            <ul>
                <li>2.1 Il sito www.mycookin.com è stato realizzato dal gestore del portale con l’intento
                    di creare un luogo di scambio e di comunicazione di ricette tra gli utenti.</li>
                <li>2.2 Il servizio fornito dal gestore del portale ha per oggetto la fruizione e la
                    condivisione a titolo gratuito di contenuti multimediali di genere gastronomico,
                    di seguito denominati “ricette”.</li>
                <li>2.3 Il portale offre altresì la possibilità di accedere, previa registrazione obbligatoria,
                    ad una community di utenti, di seguito denominata “la community”, attraverso la
                    quale è possibile realizzare il proprio profilo utente di cui all’art. 6.1, caricare
                    ricette alle condizioni e secondo i termini di cui al successivo art. 6.3, descrivere
                    e commentare le proprie ricette e le ricette caricate dagli altri utenti o dal gestore
                    del portale secondo i termini di cui al successivo art. 6.4.</li>
            </ul>
            <br />
            <br />
            Art. 3 – Contenuti del sito e relativi diritti di proprietà intellettuale
            <ul>
                <li>3.1 Tutti i contenuti del portale quali, solo a titolo esemplificativo, loghi, marchi,
                    immagini, suoni, brani, software, icone e grafici sono di proprietà di MyCookin
                    Ltd o dei loro licenzianti e come tali sono protetti dalla normativa nazionale e
                    internazionale sui diritti d’autore e i diritti connessi.</li>
                <li>3.2 I marchi deil gestore del portale presenti sul sito non possono essere utilizzati
                    senza l’espresso consenso dei titolari.</li>
                <li>3.3 La compilazione dei contenuti del portale, i codici sorgente, le formule, le
                    banche dati e ogni applicazione utilizzata nella strutturazione del portale sono
                    di proprietà di MyCookin Ltd e protetti dalla normativa nazionale ed internazionale
                    sul diritto d’autore ed i diritti connessi.</li>
                <li>3.4 il gestore del portale riconoscono e promuovono la libera condivisione dei contenuti
                    multimediali di genere enogastronomico presenti sul sito e a tal fine rilasciano
                    le ricette con licenza “Creative Commons Attribuzione-Non-Commerciale-Condividi
                    allo stesso modo 2.5 Italia License”, secondo i termini e le condizioni della stessa.</li>
                <li>3.5 L’utente del portale sarà, pertanto, libero di riprodurre, distribuire, comunicare
                    al pubblico, rappresentare, eseguire e recitare le ricette presenti sul sito con
                    l’obbligo di attribuirne la paternità al relativo autore. L’utente non potrà utilizzare
                    queste opere per fini commerciali. L’utente non potrà inoltre alterare o trasformare
                    queste opere, né usarle per crearne altre. L’eventuale pubblicazione delle stesse
                    opere su altri portali web è vietata, tale pubblicazione potrà avvenire solo in
                    seguito a espressa autorizzazione del gestore del portale.</li>
                <li>3.6 Le informazioni contenute nel sito, pur fornite in buona fede e ritenute accurate,
                    potrebbero contenere inesattezze o essere viziate da errori tipografici. Il gestore
                    del portale si riserva pertanto il diritto di modificare, aggiornare o cancellare
                    i contenuti del sito senza preavviso.</li>
                <li>3.7 il gestore del portale non è responsabile per quanto pubblicato dai lettori
                    nei post e nei commenti a ogni post. Verranno pertanto cancellati, senza alcun preavviso,
                    i post o i commenti ai post ritenuti offensivi o lesivi dell’immagine o dell’onorabilità
                    di terzi, di genere spam, razzisti o che contengano dati personali non conformi
                    al rispetto delle norme sulla privacy e, in ogni caso, ritenuti inadatti a insindacabile
                    giudizio deil gestore stessi.</li>
            </ul>
            <br />
            <br />
            Art. 4 – Utilizzo del sito
            <ul>
                <li>4.1 Il sito complessivamente inteso è destinato ad uso esclusivamente personale.</li>
                <li>4.2. È espressamente vietato danneggiare, interferire o interrompere l’accesso al
                    portale o ai contenuti dello stesso, o compiere azioni che potrebbero alterare la
                    funzionalità o interferire con l’accesso di altri utenti al sito o ai contenuti
                    presenti.</li>
                <li>4.3 È vietato utilizzare il portale o i contenuti dello stesso in modo illegale
                    o dannoso per il gestore del sito o per qualsiasi altro utente del sito.</li>
                <li>4.4 È vietato registrarsi per conto di terze persone.</li>
                <li>4.5 È vietato registrarsi più volte, ovvero creare più account per la stessa persona.</li>
                <li>4.6 È vietato l’utilizzo del portale o dei contenuti ospitati per fini commerciali
                    senza l’espressa autorizzazione deil gestore del portale.</li>
            </ul>
            <br />
            <br />
            Art. 5 – Collegamenti e links
            <ul>
                <li>5.1 Le pagine di cui si compone il portale possono consentire l’accesso, attraverso
                    i collegamenti ipertestuali dedicati, ad altri siti web o risorse in rete.</li>
                <li>5.2 Il gestore del portale non è responsabile, direttamente o indirettamente,
                    per eventuali danni subiti dagli utenti in relazione ai contenuti o alla pubblicità
                    ospitata su tali siti o per i prodotti/servizi ivi offerti e negoziati.</li>
            </ul>
            <br />
            <br />
            Art. 6 – Regole della Community
            <ul>
                <li>6.1 Procedura di registrazione utente</li>
                <li>6.1.1 Per poter accedere ai servizi della community di cui all’art. 2.3 è necessaria
                    la registrazione dell’utente al portale secondo le modalità e alle condizioni descritte
                    nel presente documento. Per poter accedere alla procedura di registrazione l’utente
                    dovrà preventivamente visionare e approvare l’informativa per il trattamento dei
                    dati personali di cui all’art. 13 del D. Lgs. n. 196/2003, disponibile in calce
                    alle presenti Condizioni Generali, costituendone parte integrante ed essenziale.
                    L’utente garantisce che i dati forniti sono veri, completi e aggiornati. L’utente
                    si impegna, in caso di variazioni, ad aggiornare tempestivamente le informazioni
                    fornite ail gestore del portale. In ogni caso il gestore del portale non potrà
                    essere ritenuto responsabile per la mancata fruizione dei servizi offerti dal sito
                    dovuta a errori o inesattezze relative ai dati personali forniti dall’utente all’atto
                    della registrazione.</li>
                <li>6.1.2 L’utente, all’inizio della procedura di registrazione dovrà indicare nome,
                    cognome, data di nascita, un proprio indirizzo email, uno username e una password
                    prescelta per accedere ai servizi della community. Il mantenimento della segretezza
                    della password è di esclusiva responsabilità dell’utente che sarà, di conseguenza,
                    responsabile di ogni attività posta in essere tramite l’utilizzo dello username
                    associato alla password prescelta. </li>
                <li>6.2 Creazione del profilo utente</li>
                <li>6.2.1 Effettuata la registrazione al sito secondo quanto previsto dal precedente
                    art. 6.1, l’utente potrà procedere alla creazione del proprio profilo utente personale.</li>
                <li>6.2.2 Il profilo utente si potrà arricchire con l’indicazione di ulteriori informazioni
                    e/o sezioni quali, a titolo esemplificativo, sesso, città, chi sono, hobby, cucina
                    preferita, piatti preferiti. L’utente potrà altresì caricare un’immagine associata
                    al proprio profilo utente. </li>
                <li>6.2.3 Tutte le informazioni fornite dall’utente potranno essere rese accessibili
                    agli altri utenti, registrati e non, del portale.</li>
                <li>6.3 Caricamento e condivisione delle ricette</li>
                <li>6.3.1 Gli utenti registrati potranno inserire, associate al proprio profilo utente,
                    le proprie ricette, in quanto autori degli stessi o avendo comunque acquisito dagli
                    autori i relativi diritti secondo le vigenti leggi nazionali e internazionali in
                    materia di diritti d’autore e diritti connessi.</li>
                <li>6.3.2 L’inserimento delle ricette comporta per gli utenti la cessione a titolo gratuito
                    in favore del gestore del portale di un diritto di licenza illimitato, irrevocabile,
                    universale, non esclusivo e completamente trasferibile di utilizzare, copiare, modificare,
                    pubblicare e visualizzare ogni contenuto caricato sul portale. L’utente cede a titolo
                    gratuito ogni diritto di sfruttamento economico sul contenuto caricato a favore
                    di MyCookin Ltd. </li>
                <li>6.3.3 L’inserimento delle ricette sul sito comporta, altresì, la rinuncia incondizionata
                    a che il nome e lo username cui sono associate le ricette caricate sul sito venga
                    riportato in occasione di ogni altro utilizzo che il gestore del sito vorrà fare
                    del materiale ceduto.</li>
                <li>6.4 Descrizione delle ricette e commenti</li>
                <li>6.4.1 L’utente potrà associare alla ricetta caricata sul sito tramite il proprio
                    profilo utente una descrizione delle modalità di preparazione della stessa con l’indicazione
                    degli ingredienti e del tempo necessario per la preparazione.</li>
                <li>6.4.2 L’utente potrà altresì esprimere giudizi e valutazioni, anche sotto forma
                    di commenti, relativi alle proprie ricette o alle altre ricette presenti e rese
                    disponibili sul portale.</li>
                <li>6.4.3 Nello svolgimento delle attività di cui ai precedenti artt. 6.4.1 e 6.4.2
                    l’utente è tenuto a utilizzare un linguaggio corretto e idoneo alle finalità descritte.
                    L’inserimento di commenti non idonei, su segnalazione secondo la procedura di cui
                    al successivo art. 6.5, quali commenti incitanti all’odio razziale, blasfemi o volgari
                    comportano l’immediata sospensione dell’accesso al sito e, in seguito alle verifiche
                    predisposte dal gestore del sito, possono comportare la cancellazione dell’account.</li>
                <li>6.5 Verifiche e procedura di segnalazione e rimozione di contenuti</li>
                <li>6.5.1 Il gestore del sito, secondo le disposizioni di cui agli artt. 14 e 15 della
                    Direttiva 2000/31/CE (recepita in Italia con D. Lgs. n. 70/2003, non è responsabile
                    delle informazioni trasmesse né delle eventuali violazioni di diritti di proprietà
                    intellettuale di terze parti o altri utenti realizzate attraverso il portale.</li>
                <li>6.5.2 il gestore del sito non esercita, pertanto, un’attività di controllo preventivo
                    sui contenuti caricati dagli utenti. Si riserva, tuttavia, il diritto di effettuare
                    tale controllo modificando, cancellando, rimuovendo i contenuti non idonei senza
                    preavviso.</li>
                <li>6.5.3 Se un utente ritiene che un materiale caricato da altro utente sul portale
                    stia violando le prescrizioni contenute nelle presenti Condizioni Generali può contattare
                    il gestore del sito tramite la compilazione dell’apposito form “Contattaci” presente
                    sul sito indicando il tipo di violazione, lo username del presunto responsabile
                    nonché l’area del portale in cui è avvenuta la violazione. Il gestore del sito, senza
                    indugio, all’esito delle opportune verifiche, assumerà i provvedimenti di cui
                    al precedente 6.5.2.</li>
            </ul>
            <br />
            <br />
            Art. 7 – Limitazioni ed esonero di responsabilità
            <ul>
                <li>7.1 L’utente riconosce e accetta espressamente che l’utilizzo del portale avviene
                    a sua sola ed esclusiva discrezione. I servizi offerti dal portale sono forniti
                    “come sono” e “come disponibili” al momento della loro fruizione.</li>
                <li>7.2 È espressamente esclusa a carico del gestore del sito qualsiasi garanzia, esplicita
                    o implicita, inclusa, a mero titolo esemplificativo, la garanzia di commerciabilità
                    o di idoneità a scopi particolari o di qualità dei contenuti del sito.</li>
                <li>7.3 Il gestore del sito non fornisce alcuna garanzia del buon funzionamento del
                    sito e dei servizi forniti.</li>
                <li>7.4 Qualsiasi materiale scaricato o altrimenti acquisito dal o per il tramite del
                    portale è ottenuto a sola ed esclusiva discrezione e a esclusivo rischio dell’utente.
                    L’utente è il solo ed esclusivo responsabile per ogni danno al proprio computer
                    o per la perdita di dati derivante dall’aver scaricato tali contenuti o materiali
                    o dall’avere utilizzato il sito e i relativi servizi.</li>
                <li>7.5 Il gestore del sito non sarà in alcun modo responsabile per eventuali danni
                    di qualunque specie e natura, anche relativi alla perdita di dati, risultanti da
                    un utilizzo improprio o scorretto del sito o dei servizi, un accesso non autorizzato
                    o un’alterazione delle trasmissioni o dei dati dell’utente, dichiarazioni o comportamenti
                    di qualunque terzo soggetto. </li>
            </ul>
            <br />
            <br />
            Art. 8 – Manleva
            <ul>
                <li>L’utente dichiara e garantisce di tenere indenne e manlevare il gestore del sito
                    da qualsiasi obbligo risarcitorio, incluse le ragionevoli spese legali, che possa
                    derivare dai contenuti trasmessi o inviati dall’utente, dall’utilizzo dei servizi
                    da parte dell’utente, dalla connessione ai servizi da parte dell’utente, da una
                    violazione delle norme che ne regolamentano l’uso, da una violazione dei diritti
                    di terzi.</li>
            </ul>
            <br />
            <br />
            Art. 9 – Clausola risolutiva espressa
            <ul>
                <li>Il gestore del portale si riserva la facoltà di risolvere di diritto il presente
                    contratto, ai sensi e per gli effetti di cui all’art. 1456 c.c., a mezzo di email
                    inviata all’utente, in caso di inadempimento di anche una soltanto delle obbligazioni
                    contenute negli artt. 4 e 6 delle presenti Condizioni Generali.</li>
            </ul>
            <br />
            <br />
            Art. 10 – Legge applicabile e foro competente
            <ul>
                <li>La legge italiana regola le presenti Condizioni Generali e le eventuali successive
                    Condizioni Particolari. Per ogni controversia derivante o connessa alle presenti
                    Condizioni Generali o a eventuali Condizioni Particolari sarà esclusivamente competente
                    il Foro di Milano.</li>
            </ul>
            <br />
            <br />
            Art. 11 – Norma finale
            <ul>
                <li>11.1 Il gestore del sito si riserva il diritto di modificare, integrare o cancellare,
                    senza fornire alcun preavviso agli utenti, le presenti Condizioni Generali o qualunque
                    parte di esse, in qualsiasi tempo, per ragioni di carattere giuridico o tecnico
                    o di altra natura.</li>
                <li>11.2 Il gestore del sito può modificare, sospendere o cessare in ogni tempo qualsiasi
                    servizio offerto senza alcun preavviso.</li>
                <li>11.3 Qualora una delle parti delle presenti Condizioni sia dichiarata invalida o
                    inefficace secondo la legge applicabile, detta clausola invalida o inefficace si
                    considererà sostituita da altra clausola valida ed efficace, in conformità con la
                    volontà delle parti così come espressa nella clausola originaria, e le restanti
                    Condizioni si considereranno comunque efficaci.</li>
                <li>Ai sensi e per gli effetti di cui agli artt. 1341 e 1342 c.c., l’utente nell’accettare
                    le presenti Condizioni Generali dichiara di aver letto, compreso e di accettare
                    le disposizioni di cui ai precedenti artt. 3, 4, 6, 9, 10 e 11. </li>
            </ul>
            <br />
            * * *
            <br />
            <br />
            Informativa sulla privacy
            <br />
            Art. 13 del D. Lgs. n. 196/2003
            <ul>
                <li>“1. L'interessato o la persona presso la quale sono raccolti i dati personali sono
                    previamente informati oralmente o per iscritto circa:</li>
                <li>a) le finalità e le modalità del trattamento cui sono destinati i dati;</li>
                <li>b) la natura obbligatoria o facoltativa del conferimento dei dati;</li>
                <li>c) le conseguenze di un eventuale rifiuto di rispondere;</li>
                <li>d) i soggetti o le categorie di soggetti ai quali i dati personali possono essere
                    comunicati o che possono venirne a conoscenza in qualità di responsabili o incaricati,
                    e l'ambito di diffusione dei dati medesimi;</li>
                <li>e) i diritti di cui all'articolo 7;</li>
                <li>f) gli estremi identificativi del titolare e, se designati, del rappresentante nel
                    territorio dello Stato ai sensi dell'articolo 5 e del responsabile. Quando il titolare
                    ha designato più responsabili è indicato almeno uno di essi, indicando il sito della
                    rete di comunicazione o le modalità attraverso le quali è conoscibile in modo agevole
                    l'elenco aggiornato dei responsabili. Quando è stato designato un responsabile per
                    il riscontro all'interessato in caso di esercizio dei diritti di cui all'articolo
                    7, è indicato tale responsabile.</li>
                <li>2. L'informativa di cui al comma 1 contiene anche gli elementi previsti da specifiche
                    disposizioni del presente codice e può non comprendere gli elementi già noti alla
                    persona che fornisce i dati o la cui conoscenza può ostacolare in concreto l'espletamento,
                    da parte di un soggetto pubblico, di funzioni ispettive o di controllo svolte per
                    finalità di difesa o sicurezza dello Stato oppure di prevenzione, accertamento o
                    repressione di reati.</li>
                <li>3. Il Garante può individuare con proprio provvedimento modalità semplificate per
                    l'informativa fornita in particolare da servizi telefonici di assistenza e informazione
                    al pubblico.</li>
                <li>4. Se i dati personali non sono raccolti presso l'interessato, l'informativa di
                    cui al comma 1, comprensiva delle categorie di dati trattati, è data al medesimo
                    interessato all'atto della registrazione dei dati o, quando è prevista la loro comunicazione,
                    non oltre la prima comunicazione.</li>
                <li>5. La disposizione di cui al comma 4 non si applica quando:</li>
                <li>a) i dati sono trattati in base ad un obbligo previsto dalla legge, da un regolamento
                    o dalla normativa comunitaria;</li>
                <li>b) i dati sono trattati ai fini dello svolgimento delle investigazioni difensive
                    di cui alla legge 7 dicembre 2000, n. 397, o, comunque, per far valere o difendere
                    un diritto in sede giudiziaria, sempre che i dati siano trattati esclusivamente
                    per tali finalità e per il periodo strettamente necessario al loro perseguimento;</li>
                <li>c) l'informativa all'interessato comporta un impiego di mezzi che il Garante, prescrivendo
                    eventuali misure appropriate, dichiari manifestamente sproporzionati rispetto al
                    diritto tutelato, ovvero si riveli, a giudizio del Garante, impossibile.</li>
                <li>5-bis. L'informativa di cui al comma 1 non è dovuta in caso di ricezione di curricula
                    spontaneamente trasmessi dagli interessati ai fini dell'eventuale instaurazione
                    di un rapporto di lavoro. Al momento del primo contatto successivo all'invio del
                    curriculum, il titolare è tenuto a fornire all'interessato, anche oralmente, una
                    informativa breve contenente almeno gli elementi di cui al comma 1, lettere a),
                    d) ed f)”.</li>
            </ul>
        </asp:Panel>
    </asp:Panel>
</asp:Content>
