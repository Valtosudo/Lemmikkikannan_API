# Lemmikkikannan_API

http://localhost:5216 on osoite

Tämä käynnistää Docker containerin ja pystyt HTTPie:llä nyt lisäämään henkilön, lemmikin ja hakemaan omistajan puhelinnumeron lemmikin nimellä

docker run -d -p 5216:5216 lemmikkikannan_api  


Henkilön lisääminen

Post http://localhost:5216/henkilot
{
  "Nimi": "nimesi",
  "Puhelin": puhelinnumero,
  "Id": 1
}

Lemmikin lisääminen 

Post http://localhost:5216/lemmikit
{
  "Nimi": "lemmikin nimi",
  "Rotu": "lemmikin rotu",
  "OmistajaId": 1
}

Puhelinnumeron saaminen lemmikin nimellä

http://localhost:5216/puhelin/lemmikin nimi


