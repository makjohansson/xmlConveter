# XML Converter

## Integration mellan två gamla system
Från radbaserat filformat till XML format. `XmlFiles` mappen innehåller ett exemple på hur xml filen som skapas ser ut, `people.xml`.

## Exekverbar version
För att köra den exekverbara versionen öppna `executable` i en terminal och kör kommandot `dotnet xmlConverter.dll`. Programmet tar två argument. Första argumentet är filvägen till den radbaserade filen som ska konverteras till XML. Andra argumentet är filvägen till vart XML filen ska skapas och dess namn.
Om inga argument anges så kommer default värden användas, `TextFiles/people.txt` som inputfil och `XmlFiles/people.xml` som outputfil. 


### Exempel på använding av programmet
`dotnet xmlConverter.dll` *// TextFiles/names.txt radbaserad textfil och utputfil kommer skapas som XmlFiles/peopel.xml*

`dotnet xmlConverter.dll rowBasedFile.csv` *// rowBasedFile.csv radbaserad textfile och outputfil kommer skapas som XmlFiles/people.xml*

`dotnet xmlConverter.dll rowBasedFile.csv XmlFiles/p.xml` *// rowBasedFile.csv radbaserad textfile och outputfil kommer skapas som XmlFiles/p.xml*