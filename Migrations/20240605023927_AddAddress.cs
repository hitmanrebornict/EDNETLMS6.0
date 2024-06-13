using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EDNETLMS.Migrations
{
	/// <inheritdoc />
	public partial class AddAddress : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RenameColumn(
				name: "Address",
				table: "Persons",
				newName: "State");

			migrationBuilder.AddColumn<string>(
				name: "AddressLine1",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "AddressLine2",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "City",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "PostalCode",
				table: "Persons",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "AddressLine1",
				table: "Persons");

			migrationBuilder.DropColumn(
				name: "AddressLine2",
				table: "Persons");

			migrationBuilder.DropColumn(
				name: "City",
				table: "Persons");

			migrationBuilder.DropColumn(
				name: "PostalCode",
				table: "Persons");

			migrationBuilder.RenameColumn(
				name: "State",
				table: "Persons",
				newName: "Address");
		}
	}
}