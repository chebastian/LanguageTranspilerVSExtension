# LanguageTranspilerVSExtension
A very specific extension that creates a key value pair textfile by searching through a cs file
By looking for ReadString and taking the first two strings found between quotes

E.g 
`ReadString("key","a value");`
will become 
`"key"="a value"`
