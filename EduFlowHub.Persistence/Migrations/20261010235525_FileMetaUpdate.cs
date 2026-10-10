using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduFlowHub.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FileMetaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollments_Users_UserId",
                table: "CourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonMaterials_Courses_CourseId",
                table: "LessonMaterials");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonMaterials_FileMetadata_FileMetadataId",
                table: "LessonMaterials");

            migrationBuilder.DropIndex(
                name: "IX_LessonMaterials_FileMetadataId",
                table: "LessonMaterials");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Course_Price_Positive",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "FileMetadataId",
                table: "LessonMaterials");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CourseEnrollments",
                newName: "StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrollments_UserId",
                table: "CourseEnrollments",
                newName: "IX_CourseEnrollments_StudentId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LessonMaterials",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "FileMetadata",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<Guid>(
                name: "LessonMaterialId",
                table: "FileMetadata",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "CourseCategories",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CourseCategories",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_FileMetadata_LessonMaterialId",
                table: "FileMetadata",
                column: "LessonMaterialId",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Course_Price_Positive",
                table: "Courses",
                sql: "\"Price\" >= 0");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollments_Users_StudentId",
                table: "CourseEnrollments",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FileMetadata_LessonMaterials_LessonMaterialId",
                table: "FileMetadata",
                column: "LessonMaterialId",
                principalTable: "LessonMaterials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonMaterials_Courses_CourseId",
                table: "LessonMaterials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEnrollments_Users_StudentId",
                table: "CourseEnrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_FileMetadata_LessonMaterials_LessonMaterialId",
                table: "FileMetadata");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonMaterials_Courses_CourseId",
                table: "LessonMaterials");

            migrationBuilder.DropIndex(
                name: "IX_FileMetadata_LessonMaterialId",
                table: "FileMetadata");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Course_Price_Positive",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "LessonMaterialId",
                table: "FileMetadata");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "CourseEnrollments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseEnrollments_StudentId",
                table: "CourseEnrollments",
                newName: "IX_CourseEnrollments_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Users",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "LessonMaterials",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<Guid>(
                name: "FileMetadataId",
                table: "LessonMaterials",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "FileMetadata",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Courses",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "CourseCategories",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CourseCategories",
                type: "character varying(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateIndex(
                name: "IX_LessonMaterials_FileMetadataId",
                table: "LessonMaterials",
                column: "FileMetadataId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Course_Price_Positive",
                table: "Courses",
                sql: "\"Price\" > 0");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEnrollments_Users_UserId",
                table: "CourseEnrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonMaterials_Courses_CourseId",
                table: "LessonMaterials",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonMaterials_FileMetadata_FileMetadataId",
                table: "LessonMaterials",
                column: "FileMetadataId",
                principalTable: "FileMetadata",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
