using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDNETLMS.Migrations
{
	public partial class AddPersonInterestedCourseInstitution : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Institutions_Countries_CountryID",
				table: "Institutions");

			migrationBuilder.DropForeignKey(
				name: "FK_Persons_Institutions_InstitutionID",
				table: "Persons");

			migrationBuilder.DropPrimaryKey(
				name: "PK_PersonInterestedInstitution",
				table: "PersonInterestedInstitution");

			migrationBuilder.DropColumn(
				name: "PIID",
				table: "PersonInterestedInstitution");

			migrationBuilder.AlterColumn<int>(
				name: "InstitutionID",
				table: "Persons",
				type: "int",
				nullable: false,
				defaultValue: 0,
				oldClrType: typeof(int),
				oldType: "int",
				oldNullable: true);

			migrationBuilder.AddPrimaryKey(
				name: "PK_PersonInterestedInstitution",
				table: "PersonInterestedInstitution",
				columns: new[] { "PersonID", "InstitutionID" });

			migrationBuilder.CreateTable(
				name: "PersonInterestedCourses",
				columns: table => new
				{
					PersonId = table.Column<int>(type: "int", nullable: false),
					CourseId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PersonInterestedCourses", x => new { x.PersonId, x.CourseId });
					table.ForeignKey(
						name: "FK_PersonInterestedCourses_Courses_CourseId",
						column: x => x.CourseId,
						principalTable: "Courses",
						principalColumn: "CourseID",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_PersonInterestedCourses_Persons_PersonId",
						column: x => x.PersonId,
						principalTable: "Persons",
						principalColumn: "PersonID",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_PersonInterestedInstitution_InstitutionID",
				table: "PersonInterestedInstitution",
				column: "InstitutionID");

			migrationBuilder.CreateIndex(
				name: "IX_PersonInterestedCourses_CourseId",
				table: "PersonInterestedCourses",
				column: "CourseId");

			migrationBuilder.AddForeignKey(
				name: "FK_Institutions_Countries_CountryID",
				table: "Institutions",
				column: "CountryID",
				principalTable: "Countries",
				principalColumn: "CountryID",
				onDelete: ReferentialAction.Restrict);

			migrationBuilder.AddForeignKey(
				name: "FK_PersonInterestedInstitution_Institutions_InstitutionID",
				table: "PersonInterestedInstitution",
				column: "InstitutionID",
				principalTable: "Institutions",
				principalColumn: "InstitutionID",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_PersonInterestedInstitution_Persons_PersonID",
				table: "PersonInterestedInstitution",
				column: "PersonID",
				principalTable: "Persons",
				principalColumn: "PersonID",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Persons_Institutions_InstitutionID",
				table: "Persons",
				column: "InstitutionID",
				principalTable: "Institutions",
				principalColumn: "InstitutionID",
				onDelete: ReferentialAction.Restrict);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropForeignKey(
				name: "FK_Institutions_Countries_CountryID",
				table: "Institutions");

			migrationBuilder.DropForeignKey(
				name: "FK_PersonInterestedInstitution_Institutions_InstitutionID",
				table: "PersonInterestedInstitution");

			migrationBuilder.DropForeignKey(
				name: "FK_PersonInterestedInstitution_Persons_PersonID",
				table: "PersonInterestedInstitution");

			migrationBuilder.DropForeignKey(
				name: "FK_Persons_Institutions_InstitutionID",
				table: "Persons");

			migrationBuilder.DropTable(
				name: "PersonInterestedCourses");

			migrationBuilder.DropPrimaryKey(
				name: "PK_PersonInterestedInstitution",
				table: "PersonInterestedInstitution");

			migrationBuilder.DropIndex(
				name: "IX_PersonInterestedInstitution_InstitutionID",
				table: "PersonInterestedInstitution");

			migrationBuilder.AlterColumn<int>(
				name: "InstitutionID",
				table: "Persons",
				type: "int",
				nullable: true,
				oldClrType: typeof(int),
				oldType: "int");

			migrationBuilder.AddColumn<int>(
				name: "PIID",
				table: "PersonInterestedInstitution",
				type: "int",
				nullable: false,
				defaultValue: 0)
				.Annotation("SqlServer:Identity", "1, 1");

			migrationBuilder.AddPrimaryKey(
				name: "PK_PersonInterestedInstitution",
				table: "PersonInterestedInstitution",
				column: "PIID");

			migrationBuilder.AddForeignKey(
				name: "FK_Institutions_Countries_CountryID",
				table: "Institutions",
				column: "CountryID",
				principalTable: "Countries",
				principalColumn: "CountryID",
				onDelete: ReferentialAction.Cascade);

			migrationBuilder.AddForeignKey(
				name: "FK_Persons_Institutions_InstitutionID",
				table: "Persons",
				column: "InstitutionID",
				principalTable: "Institutions",
				principalColumn: "InstitutionID");
		}
	}
}