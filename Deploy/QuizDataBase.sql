﻿CREATE TABLE [dbo].[Category] (
    [ID]           INT          IDENTITY(1,1) NOT NULL,
    [CategoryName] VARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);
GO

CREATE TABLE [dbo].[Quiz] (
    [ID]           INT          IDENTITY(1,1) NOT NULL,
    [QuizName]     VARCHAR (255) NOT NULL,
	[CategoryId]   INT           NOT NULL,
    PRIMARY KEY    CLUSTERED ([ID] ASC),
	FOREIGN KEY    ([CategoryId]) REFERENCES [dbo].[Category] ([ID])
);
GO

CREATE TABLE [dbo].[Question] (
    [Id]         INT          IDENTITY(1,1)  NOT NULL,
    [Content]    VARCHAR (255) NOT NULL,
    [QuizId]     INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([QuizId]) REFERENCES [dbo].[Quiz] ([ID])
);
GO

CREATE TABLE [dbo].[Answer] (
    [Id]         INT           IDENTITY(1,1) NOT NULL,
    [Content]    VARCHAR (255) NOT NULL,
    [Correct]    BIT           NOT NULL,
    [QuestionId] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([QuestionId]) REFERENCES [dbo].[Question] ([Id])
);
GO

INSERT INTO Category (CategoryName) values ('Generatory')
INSERT INTO Category (CategoryName) values ('Szyfratory')
INSERT INTO Category (CategoryName) values ('Funkcje Skrótu')
INSERT INTO Category (CategoryName) values ('Podpisy Cyfrowe')
GO

INSERT INTO Quiz (QuizName,CategoryId) values ('Startowy Quiz Generatory',1)
INSERT INTO Quiz (QuizName,CategoryId) values ('Startowy Quiz Szyfratory',2)
INSERT INTO Quiz (QuizName,CategoryId) values ('Startowy Quiz Funkcje Skrótu',3)
INSERT INTO Quiz (QuizName,CategoryId) values ('Startowy Quiz Podpisy Cyfrowe',4)
GO

INSERT INTO Question (Content,QuizId) values ('Ciąg generowany przez LFSR o n komórkach  pamięcu nazywa się maksymalnym, jeśli jego okres wynosi: ',1)
INSERT INTO Question (Content,QuizId) values ('Które z poniższych generatorów wykorzystują zmienną liczbę rejestrów LSFR?',1)
INSERT INTO Question (Content,QuizId) values ('Który z generatorów wykorzystuje trzy rejestry LFSR powiązane ze sobą nieliniowo przez multiplekser?',1)
INSERT INTO Question (Content,QuizId) values ('Ile rejestrów LFSR wykorzystuje generator rozrzedzający?',1)
INSERT INTO Question (Content,QuizId) values ('Do którego generatora pasuje poniższy opis? "Generator zbudowany z trzech rejestrów LSFR o różnej długości. LFSR-1 taktuje rejestr LFSR-2 jedynką, a rejestr LFSR-3 zerem. Ciąg wyjściowy jest sumą modulo 2 ciągów wyjściowych LSFR-2 i LFSR-3. "',1)
INSERT INTO Question (Content,QuizId) values ('Generator samoobcinający jest odmianą genaratora rozrzedzającego (obcinającego), Zamiast dwóch rejestrów LFSR, stosuje parę bitów wyjściowych jednego rejestru LFSR. W porównaniu z generatorem rozrzedzającym generator samoobcinający... ',1)
INSERT INTO Question (Content,QuizId) values ('Do którego generatora pasuje poniższy opis? "Generator wykorzystuje jeden rejestr LFSR i steruje własnym wyjściem zegarowym. Kiedy wyjście rejestru LFSR jest równe 0, wtedy ten LSFR jest taktowany d razy a gdy jest równe 1 to k razy,"',1)
INSERT INTO Question (Content,QuizId) values ('Stan początkowy n bitów, z którego rejestr rozpoczyna pracę nazywamy:',1)
GO

INSERT INTO Answer (Content,Correct,QuestionId) values ('T=2^n-1',1,1)
INSERT INTO Answer (Content,Correct,QuestionId) values ('T=2n-1',0,1)
INSERT INTO Answer (Content,Correct,QuestionId) values ('T=1-2^n',0,1)
INSERT INTO Answer (Content,Correct,QuestionId) values ('T=2^n+1',0,1)

INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator samoobcinający',0,2)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator progowy',1,2)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator Geffego',0,2)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Kaskad. Gollmana',0,2)

INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator samodecymujący Rueppela',0,3)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator samoobcinający',0,3)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator progowy',0,3)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator Geffego',1,3)

INSERT INTO Answer (Content,Correct,QuestionId) values ('dowolna liczba',0,4)
INSERT INTO Answer (Content,Correct,QuestionId) values ('dwa',1,4)
INSERT INTO Answer (Content,Correct,QuestionId) values ('trzy',0,4)
INSERT INTO Answer (Content,Correct,QuestionId) values ('zmienna liczba (nieparzysta)',0,4)

INSERT INTO Answer (Content,Correct,QuestionId) values ('Kaskada Gollmana',0,5)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator Geffego',0,5)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator samoobcinający',0,5)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generato przemienny stop-and-go',1,5)

INSERT INTO Answer (Content,Correct,QuestionId) values ('jest wolnejszy, ale ma o połowę mniejsze wymagania pamięciowe',1,6)
INSERT INTO Answer (Content,Correct,QuestionId) values ('jest szybszy, ale ma o połowę większe wymagania pamięciowe',0,6)
INSERT INTO Answer (Content,Correct,QuestionId) values ('jest szybszy i ma mniejsze wymagania pamięciowe',0,6)
INSERT INTO Answer (Content,Correct,QuestionId) values ('jest wolniejszy i ma większe wymagania pamięciowe',0,6)

INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator progowy',0,7)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator Geffego',0,7)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator samoobcinający',0,7)
INSERT INTO Answer (Content,Correct,QuestionId) values ('Generator samodecymujący Rueppela',1,7)

INSERT INTO Answer (Content,Correct,QuestionId) values ('okruszkiem',0,8)
INSERT INTO Answer (Content,Correct,QuestionId) values ('ziarnem',1,8)
INSERT INTO Answer (Content,Correct,QuestionId) values ('szczyptą',0,8)
INSERT INTO Answer (Content,Correct,QuestionId) values ('nasieniem',0,8)