--CREATE DATABASE ImageLibrary

CREATE TABLE Images(
Id UNIQUEIDENTIFIER PRIMARY kEY
,ImageName VARCHAR(MAX)
,ImagePath VARCHAR(MAX)
,ImageVirtualPath VARCHAR(MAX)
,ContentType VARCHAR(MAX)
);

--INSERT INTO Images (Id, ImageName, ImagePath) VALUES (@Id, @ImageName, @ImagePath)

SELECT * FROM Images

--DROP TABLE Images

--UPDATE Images
--SET ImagePath = 'http:\\localhost\images\api\Image\molier192638218.jpeg'
--WHERE Id = 'D6EEAE04-EB21-472A-A320-4E92808AEEE5'

----C:\GitHub_Guilherme\ImagesBackEnd\ImagesApi\Image\paisagem-para-sexo192154303.jpeg
--C:\GitHub_Guilherme\ImagesBackEnd\ImagesApi\Image\molier192638218.jpeg