using Microsoft.EntityFrameworkCore.Migrations;

namespace MedApp.DAL.Migrations
{
    public partial class SeedCaseReportsAndPatientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Patients (FullName) Values ('Kupriyanov')");

            migrationBuilder.Sql("INSERT INTO Patients (FullName) Values ('Solnzev')");

            migrationBuilder.Sql("INSERT INTO Patients (FullName) Values ('Gorohov')");

            migrationBuilder.Sql("INSERT INTO Patients (FullName) Values ('Surkov')");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('rhinitis', (SELECT Id FROM Patients WHERE FullName = 'Kupriyanov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('cancer', (SELECT Id FROM Patients WHERE FullName = 'Kupriyanov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('anaemia', (SELECT Id FROM Patients WHERE FullName = 'Kupriyanov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('migraine', (SELECT Id FROM Patients WHERE FullName = 'Kupriyanov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('hypertension', (SELECT Id FROM Patients WHERE FullName = 'Kupriyanov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('AIDS', (SELECT Id FROM Patients WHERE FullName = 'Solnzev'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('astha', (SELECT Id FROM Patients WHERE FullName = 'Solnzev'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('allergy', (SELECT Id FROM Patients WHERE FullName = 'Solnzev'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('leukemia', (SELECT Id FROM Patients WHERE FullName = 'Solnzev'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('hepatitis', (SELECT Id FROM Patients WHERE FullName = 'Solnzev'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('jaundice', (SELECT Id FROM Patients WHERE FullName = 'Gorohov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('diphtheria', (SELECT Id FROM Patients WHERE FullName = 'Gorohov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('herpes', (SELECT Id FROM Patients WHERE FullName = 'Gorohov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('haemorrhoids', (SELECT Id FROM Patients WHERE FullName = 'Gorohov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('deafness', (SELECT Id FROM Patients WHERE FullName = 'Gorohov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('alopecia', (SELECT Id FROM Patients WHERE FullName = 'Surkov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('dementia', (SELECT Id FROM Patients WHERE FullName = 'Surkov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('conjunctivitis', (SELECT Id FROM Patients WHERE FullName = 'Surkov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('typhoid', (SELECT Id FROM Patients WHERE FullName = 'Surkov'))");

            migrationBuilder.Sql("INSERT INTO CaseReports (Diagnosis, PatientId) Values ('plague', (SELECT Id FROM Patients WHERE FullName = 'Surkov'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CaseReports");

            migrationBuilder.Sql("DELETE FROM Patients");
        }
    }
}
