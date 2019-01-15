using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoWebService.Data.Migrations
{
    public partial class First_Quiz_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryName = table.Column<string>(unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuizName = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    QuizNumber = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.ID);
                    table.ForeignKey(
                        name: "FK__Quiz__CategoryId__0F624AF8",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    QuizId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Question__QuizId__123EB7A3",
                        column: x => x.QuizId,
                        principalTable: "Quiz",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Content = table.Column<string>(unicode: false, maxLength: 255, nullable: false),
                    Correct = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Answer__Question__151B244E",
                        column: x => x.QuestionId,
                        principalTable: "Question",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuizId",
                table: "Question",
                column: "QuizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_CategoryId",
                table: "Quiz",
                column: "CategoryId");

            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'9eb97efe-27e9-48a2-af7a-4c0cf0f59d35', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEDiiFQzlt5B7kS3NmYc1kiiq8m/hHIB9VeSKPFAZYrrCqDAH18fjCLdJAT7xWZA1WA==', N'5Z3TWRLL4AO7X5CANLAIGD74CDPE74QB', N'f6d6f643-ea3f-4a95-9c6a-23cf80552e25', NULL, 0, 0, NULL, 1, 0)");

            //category
            migrationBuilder.Sql("INSERT INTO Category(CategoryName) values('generatory')");
            migrationBuilder.Sql("INSERT INTO Category(CategoryName) values('szyfry blokowe')");
            migrationBuilder.Sql("INSERT INTO Category(CategoryName) values('szyfry klasyczne')");
            migrationBuilder.Sql("INSERT INTO Category(CategoryName) values('funkcje skrótu')");
            migrationBuilder.Sql("INSERT INTO Category(CategoryName) values('kryptografia asymetryczna')");
            migrationBuilder.Sql("INSERT INTO Category(CategoryName) values('szyfrowanie obrazów')");

            //Quiz
            migrationBuilder.Sql("INSERT INTO Quiz(QuizName, QuizNumber, CategoryId) values('startowy quiz generatory', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Quiz(QuizName, QuizNumber, CategoryId) values('startowy quiz szyfry blokowe', 1, 2)");
            migrationBuilder.Sql("INSERT INTO Quiz(QuizName, QuizNumber, CategoryId) values('startowy quiz szyfry klasyczne', 1, 3)");
            migrationBuilder.Sql("INSERT INTO Quiz(QuizName, QuizNumber, CategoryId) values('startowy quiz funkcje skrótu', 1, 4)");
            migrationBuilder.Sql("INSERT INTO Quiz(QuizName, QuizNumber, CategoryId) values('startowy quiz kryptografia asymetryczna', 1, 5)");
            migrationBuilder.Sql("INSERT INTO Quiz(QuizName, QuizNumber, CategoryId) values('startowy quiz szyfrowanie obrazów', 1, 6)");

            //Question
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Ciąg generowany przez LFSR o n komórkach  pamięcu nazywa się maksymalnym, jeśli jego okres wynosi: ', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Które z poniższych generatorów wykorzystują zmienną liczbę rejestrów LSFR?', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Który z generatorów wykorzystuje trzy rejestry LFSR powiązane ze sobą nieliniowo przez multiplekser?', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Ile rejestrów LFSR wykorzystuje generator rozrzedzający?', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Do którego generatora pasuje poniższy opis? \"Generator zbudowany z trzech rejestrów LSFR o różnej długości.LFSR - 1 taktuje rejestr LFSR - 2 jedynką, a rejestr LFSR - 3 zerem.Ciąg wyjściowy jest sumą modulo 2 ciągów wyjściowych LSFR - 2 i LFSR - 3. \"', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Generator samoobcinający jest odmianą genaratora rozrzedzającego (obcinającego), Zamiast dwóch rejestrów LFSR, stosuje parę bitów wyjściowych jednego rejestru LFSR. W porównaniu z generatorem rozrzedzającym generator samoobcinający... ', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Do którego generatora pasuje poniższy opis? \"Generator wykorzystuje jeden rejestr LFSR i steruje własnym wyjściem zegarowym.Kiedy wyjście rejestru LFSR jest równe 0, wtedy ten LSFR jest taktowany d razy a gdy jest równe 1 to k razy, \"', 1)");
            migrationBuilder.Sql("INSERT INTO Question(Content, QuizId) values(N'Stan początkowy n bitów, z którego rejestr rozpoczyna pracę nazywamy:', 1)");

            //Answer
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('T=2^n-1', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('T=2n-1', 0, 1)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('T=1-2^n', 0, 1)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('T=2^n+1', 0, 1)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator samoobcinający', 0, 2)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator progowy', 1, 2)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator Geffego', 0, 2)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Kaskad. Gollmana', 0, 2)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator samodecymujący Rueppela', 0, 3)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator samoobcinający', 0, 3)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator progowy', 0, 3)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator Geffego', 1, 3)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('dowolna liczba', 0, 4)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('dwa', 1, 4)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('trzy', 0, 4)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('zmienna liczba (nieparzysta)', 0, 4)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Kaskada Gollmana', 0, 5)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator Geffego', 0, 5)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator samoobcinający', 0, 5)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generato przemienny stop-and-go', 1, 5)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('jest wolnejszy, ale ma o połowę mniejsze wymagania pamięciowe', 1, 6)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('jest szybszy, ale ma o połowę większe wymagania pamięciowe', 0, 6)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('jest szybszy i ma mniejsze wymagania pamięciowe', 0, 6)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('jest wolniejszy i ma większe wymagania pamięciowe', 0, 6)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator progowy', 0, 7)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator Geffego', 0, 7)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator samoobcinający', 0, 7)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('Generator samodecymujący Rueppela', 1, 7)");

            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('okruszkiem', 0, 8)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('ziarnem', 1, 8)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('szczyptą', 0, 8)");
            migrationBuilder.Sql("INSERT INTO Answer(Content, Correct, QuestionId) values('nasieniem', 0, 8)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Category");
        }


    }
}
