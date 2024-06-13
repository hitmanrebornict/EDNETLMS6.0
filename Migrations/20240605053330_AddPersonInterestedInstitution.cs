using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDNETLMS.Migrations
{
	/// <inheritdoc />
	public partial class AddPersonInterestedInstitution : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "Country",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "EmergencyContactEmailAddress",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "EmergencyContactName",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "EmergencyContactPhoneNum",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.CreateTable(
				name: "PersonInterestedInstitution",
				columns: table => new
				{
					PIID = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					PersonID = table.Column<int>(type: "int", nullable: false),
					InstitutionID = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PersonInterestedInstitution", x => x.PIID);
				});
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "PersonInterestedInstitution");

			migrationBuilder.DropColumn(
				name: "Country",
				table: "Persons");

			migrationBuilder.DropColumn(
				name: "EmergencyContactEmailAddress",
				table: "Persons");

			migrationBuilder.DropColumn(
				name: "EmergencyContactName",
				table: "Persons");

			migrationBuilder.DropColumn(
				name: "EmergencyContactPhoneNum",
				table: "Persons");
		}
	}
}