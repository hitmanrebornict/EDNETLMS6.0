using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDNETLMS.Migrations
{
	/// <inheritdoc />
	public partial class DatabaseCreation : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Countries",
				columns: table => new
				{
					CountryID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Countries", x => x.CountryID);
				});

			migrationBuilder.CreateTable(
				name: "Courses",
				columns: table => new
				{
					CourseID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CourseDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Courses", x => x.CourseID);
				});

			migrationBuilder.CreateTable(
				name: "Institutions",
				columns: table => new
				{
					InstitutionID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					InstitutionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					CountryID = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Institutions", x => x.InstitutionID);
					table.ForeignKey(
						name: "FK_Institutions_Countries_CountryID",
						column: x => x.CountryID,
						principalTable: "Countries",
						principalColumn: "CountryID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Persons",
				columns: table => new
				{
					PersonID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					FamilyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					GivenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
					DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
					Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
					TelNum = table.Column<string>(type: "nvarchar(max)", nullable: false),
					EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
					EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
					InstitutionID = table.Column<int>(type: "int", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Persons", x => x.PersonID);
					table.ForeignKey(
						name: "FK_Persons_Institutions_InstitutionID",
						column: x => x.InstitutionID,
						principalTable: "Institutions",
						principalColumn: "InstitutionID");
				});

			migrationBuilder.CreateTable(
				name: "Leads",
				columns: table => new
				{
					LeadID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					PersonID = table.Column<int>(type: "int", nullable: false),
					CourseID = table.Column<int>(type: "int", nullable: false),
					PIC = table.Column<string>(type: "nvarchar(max)", nullable: false),
					HowDoYouKnowUs = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LeadStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Leads", x => x.LeadID);
					table.ForeignKey(
						name: "FK_Leads_Courses_CourseID",
						column: x => x.CourseID,
						principalTable: "Courses",
						principalColumn: "CourseID",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Leads_Persons_PersonID",
						column: x => x.PersonID,
						principalTable: "Persons",
						principalColumn: "PersonID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "LeadCatchUpStatus",
				columns: table => new
				{
					LeadCatchUpStatusID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					LeadID = table.Column<int>(type: "int", nullable: false),
					CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					Done = table.Column<bool>(type: "bit", nullable: false),
					DoneDate = table.Column<DateTime>(type: "datetime2", nullable: true),
					LeadStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
					LeadCatchUpRemark = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_LeadCatchUpStatus", x => x.LeadCatchUpStatusID);
					table.ForeignKey(
						name: "FK_LeadCatchUpStatus_Leads_LeadID",
						column: x => x.LeadID,
						principalTable: "Leads",
						principalColumn: "LeadID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Institutions_CountryID",
				table: "Institutions",
				column: "CountryID");

			migrationBuilder.CreateIndex(
				name: "IX_LeadCatchUpStatus_LeadID",
				table: "LeadCatchUpStatus",
				column: "LeadID");

			migrationBuilder.CreateIndex(
				name: "IX_Leads_CourseID",
				table: "Leads",
				column: "CourseID");

			migrationBuilder.CreateIndex(
				name: "IX_Leads_PersonID",
				table: "Leads",
				column: "PersonID");

			migrationBuilder.CreateIndex(
				name: "IX_Persons_InstitutionID",
				table: "Persons",
				column: "InstitutionID");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "LeadCatchUpStatus");

			migrationBuilder.DropTable(
				name: "Leads");

			migrationBuilder.DropTable(
				name: "Courses");

			migrationBuilder.DropTable(
				name: "Persons");

			migrationBuilder.DropTable(
				name: "Institutions");

			migrationBuilder.DropTable(
				name: "Countries");
		}
	}
}