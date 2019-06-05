using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace inventory_dot_core.Migrations.inventory
{
    public partial class tiers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "inventory");

            migrationBuilder.CreateSequence(
                name: "ab_id_seq");

            migrationBuilder.CreateSequence(
                name: "ac_id_seq");

            migrationBuilder.CreateSequence(
                name: "acc_id_seq");

            migrationBuilder.CreateSequence(
                name: "ap_id_seq");

            migrationBuilder.CreateSequence(
                name: "at_id_seq");

            migrationBuilder.CreateSequence(
                name: "dep_id_seq");

            migrationBuilder.CreateSequence(
                name: "empl_id_seq");

            migrationBuilder.CreateSequence(
                name: "house_id_seq");

            migrationBuilder.CreateSequence(
                name: "office_id_seq");

            migrationBuilder.CreateSequence(
                name: "pos_id_seq");

            migrationBuilder.CreateSequence(
                name: "rel_hard_empl_id_seq");

            migrationBuilder.CreateSequence(
                name: "rel_office_resp_seq");

            migrationBuilder.CreateSequence(
                name: "rel_soft_hard_seq");

            migrationBuilder.CreateSequence(
                name: "whard_id_seq");

            migrationBuilder.CreateSequence(
                name: "wsoft_id_seq");

            migrationBuilder.CreateSequence(
                name: "wtype_id_seq");

            migrationBuilder.CreateTable(
                name: "AccountingTires",
                schema: "inventory",
                columns: table => new
                {
                    at_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.at_id_seq'::regclass)"),
                    at_code = table.Column<string>(maxLength: 255, nullable: true),
                    at_name = table.Column<string>(maxLength: 255, nullable: true),
                    at_count = table.Column<int>(nullable: false),
                    at_cost = table.Column<decimal>(type: "numeric(8,2)", nullable: true),
                    at_iauto = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AccountingTires_pkey", x => x.at_id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "inventory",
                columns: table => new
                {
                    account_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.acc_id_seq'::regclass)"),
                    account_user_login = table.Column<string>(maxLength: 255, nullable: true),
                    account_pass = table.Column<string>(maxLength: 255, nullable: true),
                    account_permission = table.Column<string>(maxLength: 4, nullable: true),
                    account_employee_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Accounts_pkey", x => x.account_id);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "inventory",
                columns: table => new
                {
                    region_id = table.Column<int>(nullable: false),
                    region_name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.region_id);
                });

            migrationBuilder.CreateTable(
                name: "Wealth_Categories",
                schema: "inventory",
                columns: table => new
                {
                    wcat_id = table.Column<int>(nullable: false),
                    wcatname = table.Column<string>(maxLength: 255, nullable: false),
                    wcatnotes = table.Column<string>(maxLength: 4000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Wealth_Categories_pkey", x => x.wcat_id);
                });

            migrationBuilder.CreateTable(
                name: "Wealth_Types",
                schema: "inventory",
                columns: table => new
                {
                    wtype_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.wtype_id_seq'::regclass)"),
                    wtype_name = table.Column<string>(maxLength: 64, nullable: false),
                    wtype_notes = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Wealth_Types_pkey", x => x.wtype_id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "inventory",
                columns: table => new
                {
                    department_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.dep_id_seq'::regclass)"),
                    department_name = table.Column<string>(maxLength: 255, nullable: true),
                    department_notes = table.Column<string>(maxLength: 4000, nullable: true),
                    department_region_id = table.Column<int>(nullable: false),
                    department_parent_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Departments_pkey", x => x.department_id);
                    table.ForeignKey(
                        name: "departments_departments_fk",
                        column: x => x.department_parent_id,
                        principalSchema: "inventory",
                        principalTable: "Departments",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "departments_region_fk",
                        column: x => x.department_region_id,
                        principalSchema: "inventory",
                        principalTable: "Region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Houses",
                schema: "inventory",
                columns: table => new
                {
                    houses_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.house_id_seq'::regclass)"),
                    houses_name = table.Column<string>(maxLength: 255, nullable: true),
                    houses_rem = table.Column<string>(maxLength: 255, nullable: true),
                    houses_region_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.houses_id);
                    table.ForeignKey(
                        name: "houses_region_fk",
                        column: x => x.houses_region_id,
                        principalSchema: "inventory",
                        principalTable: "Region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Wealth_Software",
                schema: "inventory",
                columns: table => new
                {
                    wsoft_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.wsoft_id_seq'::regclass)"),
                    wsoft_inumber = table.Column<string>(maxLength: 30, nullable: true),
                    wsoft_fnumber = table.Column<string>(maxLength: 65, nullable: true),
                    wsoft_wcat_id = table.Column<int>(nullable: true),
                    wsoft_wtype_id = table.Column<int>(nullable: true),
                    wsoft_name = table.Column<string>(maxLength: 255, nullable: false),
                    wsoft_date_of_adoption = table.Column<DateTime>(type: "date", nullable: true),
                    wsoft_initial_cost = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    wsoft_residual_value = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    wsoft_region_id = table.Column<int>(nullable: false),
                    wsoft_note = table.Column<string>(maxLength: 255, nullable: true),
                    wsoft_archiv = table.Column<int>(nullable: true, defaultValueSql: "0"),
                    wsoft_create_date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "now()"),
                    wsoft_cnt = table.Column<int>(nullable: false, defaultValueSql: "1")
                        .Annotation("Npgsql:Comment", "Количество лицензий")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Wealth_Software_pkey", x => x.wsoft_id);
                    table.ForeignKey(
                        name: "wealth_software_region_fk",
                        column: x => x.wsoft_region_id,
                        principalSchema: "inventory",
                        principalTable: "Region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "wealth_software_wealth_categories_fk",
                        column: x => x.wsoft_wcat_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Categories",
                        principalColumn: "wcat_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "wealth_software_wealth_types_fk",
                        column: x => x.wsoft_wtype_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Types",
                        principalColumn: "wtype_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "inventory",
                columns: table => new
                {
                    position_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.pos_id_seq'::regclass)"),
                    position_name = table.Column<string>(maxLength: 255, nullable: true),
                    position_notes = table.Column<string>(maxLength: 255, nullable: true),
                    position_department_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Positions_pkey", x => x.position_id);
                    table.ForeignKey(
                        name: "positions_departments_fk",
                        column: x => x.position_department_id,
                        principalSchema: "inventory",
                        principalTable: "Departments",
                        principalColumn: "department_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                schema: "inventory",
                columns: table => new
                {
                    office_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.office_id_seq'::regclass)"),
                    office_name = table.Column<string>(maxLength: 255, nullable: false),
                    office_notes = table.Column<string>(maxLength: 4000, nullable: true),
                    office_houses_id = table.Column<int>(nullable: false),
                    office_is_store = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Offices_pkey", x => x.office_id);
                    table.ForeignKey(
                        name: "offices_houses_fk",
                        column: x => x.office_houses_id,
                        principalSchema: "inventory",
                        principalTable: "Houses",
                        principalColumn: "houses_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "inventory",
                columns: table => new
                {
                    employee_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.empl_id_seq'::regclass)"),
                    employee_firstname = table.Column<string>(maxLength: 255, nullable: false),
                    employee_lastname = table.Column<string>(maxLength: 255, nullable: false),
                    employee_middlename = table.Column<string>(maxLength: 30, nullable: false),
                    employee_phone_home = table.Column<string>(maxLength: 255, nullable: true),
                    employee_email = table.Column<string>(maxLength: 255, nullable: true),
                    employee_position_id = table.Column<int>(nullable: false),
                    employee_office_id = table.Column<int>(nullable: false),
                    employee_phone_work = table.Column<string>(maxLength: 255, nullable: true),
                    employee_note = table.Column<string>(maxLength: 255, nullable: true),
                    employee_full_fio = table.Column<string>(maxLength: 255, nullable: true),
                    employee_is_chief = table.Column<int>(nullable: true),
                    employee_is_respons = table.Column<int>(nullable: true),
                    user_id = table.Column<int>(nullable: true),
                    employee_is_mol = table.Column<int>(nullable: true),
                    employee_region_id = table.Column<int>(nullable: false, defaultValueSql: "54")
                },
                constraints: table =>
                {
                    table.PrimaryKey("Employees_pkey", x => x.employee_id);
                    table.ForeignKey(
                        name: "employees_offices_fk",
                        column: x => x.employee_office_id,
                        principalSchema: "inventory",
                        principalTable: "Offices",
                        principalColumn: "office_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "employees_positions_fk",
                        column: x => x.employee_position_id,
                        principalSchema: "inventory",
                        principalTable: "Positions",
                        principalColumn: "position_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "employees_region_fk",
                        column: x => x.employee_region_id,
                        principalSchema: "inventory",
                        principalTable: "Region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Office_Respons_Employee",
                schema: "inventory",
                columns: table => new
                {
                    roe_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.rel_office_resp_seq'::regclass)"),
                    roe_office_id = table.Column<int>(nullable: false),
                    roe_employee_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Rel_Office_Respons_Employee_pkey", x => x.roe_id);
                    table.ForeignKey(
                        name: "rel_office_respons_employee_employees_fk",
                        column: x => x.roe_employee_id,
                        principalSchema: "inventory",
                        principalTable: "Employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "rel_office_respons_employee_offices_fk",
                        column: x => x.roe_office_id,
                        principalSchema: "inventory",
                        principalTable: "Offices",
                        principalColumn: "office_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Wealth_Hardware",
                schema: "inventory",
                columns: table => new
                {
                    whard_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.whard_id_seq'::regclass)"),
                    whard_inumber = table.Column<string>(maxLength: 30, nullable: true),
                    whard_fnumber = table.Column<string>(maxLength: 65, nullable: true),
                    whard_wcat_id = table.Column<int>(nullable: false),
                    whard_wtype_id = table.Column<int>(nullable: false),
                    whard_name = table.Column<string>(maxLength: 255, nullable: true),
                    whard_date_of_adoption = table.Column<DateTime>(type: "date", nullable: true),
                    whard_initial_cost = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    whard_residual_value = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    whard_office_id = table.Column<int>(nullable: false),
                    whard_note = table.Column<string>(maxLength: 255, nullable: true),
                    whard_archiv = table.Column<int>(nullable: true, defaultValueSql: "0"),
                    whard_create_date = table.Column<DateTime>(type: "date", nullable: true, defaultValueSql: "now()"),
                    whard_mol_employee_id = table.Column<int>(nullable: true),
                    whard_region_id = table.Column<int>(nullable: true),
                    is_soft_deployed = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Wealth_Hardware_pkey", x => x.whard_id);
                    table.ForeignKey(
                        name: "wealth_hardware_employees_fk",
                        column: x => x.whard_mol_employee_id,
                        principalSchema: "inventory",
                        principalTable: "Employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "wealth_hardware_offices_fk",
                        column: x => x.whard_office_id,
                        principalSchema: "inventory",
                        principalTable: "Offices",
                        principalColumn: "office_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "wealth_hardware_region_fk",
                        column: x => x.whard_region_id,
                        principalSchema: "inventory",
                        principalTable: "Region",
                        principalColumn: "region_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "wealth_hardware_wealth_categories_fk",
                        column: x => x.whard_wcat_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Categories",
                        principalColumn: "wcat_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "wealth_hardware_wealth_types_fk",
                        column: x => x.whard_wtype_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Types",
                        principalColumn: "wtype_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Accounting_Phones",
                schema: "inventory",
                columns: table => new
                {
                    ap_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.ap_id_seq'::regclass)"),
                    ap_phone_hw_id = table.Column<int>(nullable: false),
                    ap_inumber = table.Column<string>(maxLength: 30, nullable: false),
                    ap_tnumber = table.Column<string>(maxLength: 17, nullable: true),
                    ap_imei = table.Column<string>(maxLength: 15, nullable: true),
                    ap_employee_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Accounting_Phones_pkey", x => x.ap_id);
                    table.ForeignKey(
                        name: "accounting_phones_employees_fk",
                        column: x => x.ap_employee_id,
                        principalSchema: "inventory",
                        principalTable: "Employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "accounting_phones_wealth_hardware_fk",
                        column: x => x.ap_phone_hw_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AccountingBatteries",
                schema: "inventory",
                columns: table => new
                {
                    ab_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.ab_id_seq'::regclass)"),
                    ab_bat_whard_id = table.Column<int>(nullable: false),
                    ab_whard_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AccountingBatteries_pkey", x => x.ab_id);
                    table.ForeignKey(
                        name: "accountingbatteries_wealth_hardware_fk_bat",
                        column: x => x.ab_bat_whard_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "accountingbatteries_wealth_hardware_fk",
                        column: x => x.ab_whard_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AccountingCartridges",
                schema: "inventory",
                columns: table => new
                {
                    ac_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.ac_id_seq'::regclass)"),
                    ac_cart_whard_id = table.Column<int>(nullable: false),
                    ac_whard_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("AccountingCartridges_pkey", x => x.ac_id);
                    table.ForeignKey(
                        name: "accountingcartridges_wealth_hardware_fk_cart",
                        column: x => x.ac_cart_whard_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "accountingcartridges_wealth_hardware_fk",
                        column: x => x.ac_whard_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Hardware_Employee",
                schema: "inventory",
                columns: table => new
                {
                    relhe_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.rel_hard_empl_id_seq'::regclass)"),
                    relhe_employee_id = table.Column<int>(nullable: false),
                    relhe_whard_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Rel_Hardware_Employee_pkey", x => x.relhe_id);
                    table.ForeignKey(
                        name: "rel_hardware_employee_employees_fk",
                        column: x => x.relhe_employee_id,
                        principalSchema: "inventory",
                        principalTable: "Employees",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "rel_hardware_employee_wealth_hardware_fk",
                        column: x => x.relhe_whard_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rel_Software_Hardware",
                schema: "inventory",
                columns: table => new
                {
                    relsh_id = table.Column<int>(nullable: false, defaultValueSql: "nextval('inventory.rel_soft_hard_seq'::regclass)"),
                    relsh_wsoft_id = table.Column<int>(nullable: false),
                    relsh_whard_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Rel_Software_Hardware_pkey", x => x.relsh_id);
                    table.ForeignKey(
                        name: "rel_software_hardware_wealth_hardware_fk",
                        column: x => x.relsh_whard_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Hardware",
                        principalColumn: "whard_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "rel_software_hardware_wealth_software_fk",
                        column: x => x.relsh_wsoft_id,
                        principalSchema: "inventory",
                        principalTable: "Wealth_Software",
                        principalColumn: "wsoft_id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Phones_ap_employee_id",
                schema: "inventory",
                table: "Accounting_Phones",
                column: "ap_employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_Accounting_Phones_ap_phone_hw_id",
                schema: "inventory",
                table: "Accounting_Phones",
                column: "ap_phone_hw_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingBatteries_ab_bat_whard_id",
                schema: "inventory",
                table: "AccountingBatteries",
                column: "ab_bat_whard_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingBatteries_ab_whard_id",
                schema: "inventory",
                table: "AccountingBatteries",
                column: "ab_whard_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingCartridges_ac_cart_whard_id",
                schema: "inventory",
                table: "AccountingCartridges",
                column: "ac_cart_whard_id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountingCartridges_ac_whard_id",
                schema: "inventory",
                table: "AccountingCartridges",
                column: "ac_whard_id");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_department_parent_id",
                schema: "inventory",
                table: "Departments",
                column: "department_parent_id");

            migrationBuilder.CreateIndex(
                name: "departments_department_region_id_idx",
                schema: "inventory",
                table: "Departments",
                column: "department_region_id");

            migrationBuilder.CreateIndex(
                name: "employees_employee_full_fio_idx",
                schema: "inventory",
                table: "Employees",
                column: "employee_full_fio");

            migrationBuilder.CreateIndex(
                name: "employees_employee_is_mol_idx",
                schema: "inventory",
                table: "Employees",
                column: "employee_is_mol");

            migrationBuilder.CreateIndex(
                name: "employees_employee_is_respons_idx",
                schema: "inventory",
                table: "Employees",
                column: "employee_is_respons");

            migrationBuilder.CreateIndex(
                name: "employees_employee_lastname_idx",
                schema: "inventory",
                table: "Employees",
                column: "employee_lastname");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_employee_office_id",
                schema: "inventory",
                table: "Employees",
                column: "employee_office_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_employee_position_id",
                schema: "inventory",
                table: "Employees",
                column: "employee_position_id");

            migrationBuilder.CreateIndex(
                name: "employees_employee_region_id_idx",
                schema: "inventory",
                table: "Employees",
                column: "employee_region_id");

            migrationBuilder.CreateIndex(
                name: "houses_houses_name_idx",
                schema: "inventory",
                table: "Houses",
                column: "houses_name");

            migrationBuilder.CreateIndex(
                name: "fki_Houses_Region_id",
                schema: "inventory",
                table: "Houses",
                column: "houses_region_id");

            migrationBuilder.CreateIndex(
                name: "IX_Offices_office_houses_id",
                schema: "inventory",
                table: "Offices",
                column: "office_houses_id");

            migrationBuilder.CreateIndex(
                name: "offices_office_name_idx",
                schema: "inventory",
                table: "Offices",
                column: "office_name");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_position_department_id",
                schema: "inventory",
                table: "Positions",
                column: "position_department_id");

            migrationBuilder.CreateIndex(
                name: "positions_position_name_idx",
                schema: "inventory",
                table: "Positions",
                column: "position_name");

            migrationBuilder.CreateIndex(
                name: "rel_hardware_employee_relhe_employee_id_idx",
                schema: "inventory",
                table: "Rel_Hardware_Employee",
                column: "relhe_employee_id");

            migrationBuilder.CreateIndex(
                name: "rel_hardware_employee_relhe_whard_id_idx",
                schema: "inventory",
                table: "Rel_Hardware_Employee",
                column: "relhe_whard_id");

            migrationBuilder.CreateIndex(
                name: "rel_office_respons_employee_roe_employee_id_idx",
                schema: "inventory",
                table: "Rel_Office_Respons_Employee",
                column: "roe_employee_id");

            migrationBuilder.CreateIndex(
                name: "rel_office_respons_employee_roe_office_id_idx",
                schema: "inventory",
                table: "Rel_Office_Respons_Employee",
                column: "roe_office_id");

            migrationBuilder.CreateIndex(
                name: "rel_software_hardware_relsh_whard_id_idx",
                schema: "inventory",
                table: "Rel_Software_Hardware",
                column: "relsh_whard_id");

            migrationBuilder.CreateIndex(
                name: "rel_software_hardware_relsh_wsoft_id_idx",
                schema: "inventory",
                table: "Rel_Software_Hardware",
                column: "relsh_wsoft_id");

            migrationBuilder.CreateIndex(
                name: "wealth_hardware_whard_fnumber_idx",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_fnumber");

            migrationBuilder.CreateIndex(
                name: "wealth_hardware_whard_inumber_idx",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_inumber");

            migrationBuilder.CreateIndex(
                name: "IX_Wealth_Hardware_whard_mol_employee_id",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_mol_employee_id");

            migrationBuilder.CreateIndex(
                name: "wealth_hardware_whard_name_idx",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_name");

            migrationBuilder.CreateIndex(
                name: "IX_Wealth_Hardware_whard_office_id",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_office_id");

            migrationBuilder.CreateIndex(
                name: "wealth_hardware_whard_region_id_idx",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_region_id");

            migrationBuilder.CreateIndex(
                name: "wealth_hardware_whard_wcat_id_idx",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_wcat_id");

            migrationBuilder.CreateIndex(
                name: "wealth_hardware_whard_wtype_id_idx",
                schema: "inventory",
                table: "Wealth_Hardware",
                column: "whard_wtype_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wealth_Software_wsoft_region_id",
                schema: "inventory",
                table: "Wealth_Software",
                column: "wsoft_region_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wealth_Software_wsoft_wcat_id",
                schema: "inventory",
                table: "Wealth_Software",
                column: "wsoft_wcat_id");

            migrationBuilder.CreateIndex(
                name: "IX_Wealth_Software_wsoft_wtype_id",
                schema: "inventory",
                table: "Wealth_Software",
                column: "wsoft_wtype_id");

            migrationBuilder.CreateIndex(
                name: "wealth_types_wtype_name_idx",
                schema: "inventory",
                table: "Wealth_Types",
                column: "wtype_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounting_Phones",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "AccountingBatteries",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "AccountingCartridges",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "AccountingTires",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Rel_Hardware_Employee",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Rel_Office_Respons_Employee",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Rel_Software_Hardware",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Wealth_Hardware",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Wealth_Software",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Wealth_Categories",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Wealth_Types",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Offices",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Houses",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "inventory");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "inventory");

            migrationBuilder.DropSequence(
                name: "ab_id_seq");

            migrationBuilder.DropSequence(
                name: "ac_id_seq");

            migrationBuilder.DropSequence(
                name: "acc_id_seq");

            migrationBuilder.DropSequence(
                name: "ap_id_seq");

            migrationBuilder.DropSequence(
                name: "at_id_seq");

            migrationBuilder.DropSequence(
                name: "dep_id_seq");

            migrationBuilder.DropSequence(
                name: "empl_id_seq");

            migrationBuilder.DropSequence(
                name: "house_id_seq");

            migrationBuilder.DropSequence(
                name: "office_id_seq");

            migrationBuilder.DropSequence(
                name: "pos_id_seq");

            migrationBuilder.DropSequence(
                name: "rel_hard_empl_id_seq");

            migrationBuilder.DropSequence(
                name: "rel_office_resp_seq");

            migrationBuilder.DropSequence(
                name: "rel_soft_hard_seq");

            migrationBuilder.DropSequence(
                name: "whard_id_seq");

            migrationBuilder.DropSequence(
                name: "wsoft_id_seq");

            migrationBuilder.DropSequence(
                name: "wtype_id_seq");
        }
    }
}
