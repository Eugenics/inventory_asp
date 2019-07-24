using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace inventory_dot_core.Models
{
    public partial class InventoryContext : DbContext
    {
        public InventoryContext()
        {
        }

        public InventoryContext(DbContextOptions<InventoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountingBatteries> AccountingBatteries { get; set; }
        public virtual DbSet<AccountingCartridges> AccountingCartridges { get; set; }
        public virtual DbSet<AccountingPhones> AccountingPhones { get; set; }
        public virtual DbSet<AccountingTires> AccountingTires { get; set; }
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<Houses> Houses { get; set; }
        public virtual DbSet<Offices> Offices { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RelHardwareEmployee> RelHardwareEmployee { get; set; }
        public virtual DbSet<RelOfficeResponsEmployee> RelOfficeResponsEmployee { get; set; }
        public virtual DbSet<RelSoftwareHardware> RelSoftwareHardware { get; set; }
        public virtual DbSet<WealthCategories> WealthCategories { get; set; }
        public virtual DbSet<WealthHardware> WealthHardware { get; set; }
        public virtual DbSet<WealthSoftware> WealthSoftware { get; set; }
        public virtual DbSet<WealthTypes> WealthTypes { get; set; }

        // Unable to generate entity type for table 'inventory.Temp_Hardware'. Please see the warning messages.

        #region added manualy
        public IConfiguration Configuration { get; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(Configuration.GetConnectionString("inventoryDataBase"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AccountingBatteries>(entity =>
            {
                entity.HasKey(e => e.AbId)
                    .HasName("AccountingBatteries_pkey");

                entity.ToTable("AccountingBatteries", "inventory");

                entity.Property(e => e.AbId)
                    .HasColumnName("ab_id")
                    .HasDefaultValueSql("nextval('inventory.ab_id_seq'::regclass)");

                entity.Property(e => e.AbBatWhardId).HasColumnName("ab_bat_whard_id");

                entity.Property(e => e.AbWhardId).HasColumnName("ab_whard_id");

                entity.HasOne(d => d.AbBatWhard)
                    .WithMany(p => p.AccountingBatteriesAbBatWhard)
                    .HasForeignKey(d => d.AbBatWhardId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("accountingbatteries_wealth_hardware_fk_bat");

                entity.HasOne(d => d.AbWhard)
                    .WithMany(p => p.AccountingBatteriesAbWhard)
                    .HasForeignKey(d => d.AbWhardId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("accountingbatteries_wealth_hardware_fk");
            });

            modelBuilder.Entity<AccountingCartridges>(entity =>
            {
                entity.HasKey(e => e.AcId)
                    .HasName("AccountingCartridges_pkey");

                entity.ToTable("AccountingCartridges", "inventory");

                entity.Property(e => e.AcId)
                    .HasColumnName("ac_id")
                    .HasDefaultValueSql("nextval('inventory.ac_id_seq'::regclass)");

                entity.Property(e => e.AcCartWhardId).HasColumnName("ac_cart_whard_id");

                entity.Property(e => e.AcWhardId).HasColumnName("ac_whard_id");

                entity.HasOne(d => d.AcCartWhard)
                    .WithMany(p => p.AccountingCartridgesAcCartWhard)
                    .HasForeignKey(d => d.AcCartWhardId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("accountingcartridges_wealth_hardware_fk_cart");

                entity.HasOne(d => d.AcWhard)
                    .WithMany(p => p.AccountingCartridgesAcWhard)
                    .HasForeignKey(d => d.AcWhardId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("accountingcartridges_wealth_hardware_fk");
            });

            modelBuilder.Entity<AccountingPhones>(entity =>
            {
                entity.HasKey(e => e.ApId)
                    .HasName("Accounting_Phones_pkey");

                entity.ToTable("Accounting_Phones", "inventory");

                entity.Property(e => e.ApId)
                    .HasColumnName("ap_id")
                    .HasDefaultValueSql("nextval('inventory.ap_id_seq'::regclass)");

                entity.Property(e => e.ApEmployeeId).HasColumnName("ap_employee_id");

                entity.Property(e => e.ApImei)
                    .HasColumnName("ap_imei")
                    .HasMaxLength(15);

                entity.Property(e => e.ApInumber)
                    .IsRequired()
                    .HasColumnName("ap_inumber")
                    .HasMaxLength(30);

                entity.Property(e => e.ApPhoneHwId).HasColumnName("ap_phone_hw_id");

                entity.Property(e => e.ApTnumber)
                    .HasColumnName("ap_tnumber")
                    .HasMaxLength(17);

                entity.HasOne(d => d.ApEmployee)
                    .WithMany(p => p.AccountingPhones)
                    .HasForeignKey(d => d.ApEmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("accounting_phones_employees_fk");

                entity.HasOne(d => d.ApPhoneHw)
                    .WithMany(p => p.AccountingPhones)
                    .HasForeignKey(d => d.ApPhoneHwId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("accounting_phones_wealth_hardware_fk");
            });

            modelBuilder.Entity<AccountingTires>(entity =>
            {
                entity.HasKey(e => e.AtId)
                    .HasName("AccountingTires_pkey");

                entity.ToTable("AccountingTires", "inventory");

                entity.Property(e => e.AtId)
                    .HasColumnName("at_id")
                    .HasDefaultValueSql("nextval('inventory.at_id_seq'::regclass)");

                entity.Property(e => e.AtCode)
                    .HasColumnName("at_code")
                    .HasMaxLength(255);

                entity.Property(e => e.AtCost)
                    .HasColumnName("at_cost")
                    .HasColumnType("numeric(8,2)");

                entity.Property(e => e.AtCount).HasColumnName("at_count");

                entity.Property(e => e.AtIauto)
                    .HasColumnName("at_iauto")
                    .HasMaxLength(255);

                entity.Property(e => e.AtName)
                    .HasColumnName("at_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.HasKey(e => e.AccountId)
                    .HasName("Accounts_pkey");

                entity.ToTable("Accounts", "inventory");

                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .HasDefaultValueSql("nextval('inventory.acc_id_seq'::regclass)");

                entity.Property(e => e.AccountEmployeeId).HasColumnName("account_employee_id");

                entity.Property(e => e.AccountPass)
                    .HasColumnName("account_pass")
                    .HasMaxLength(255);

                entity.Property(e => e.AccountPermission)
                    .HasColumnName("account_permission")
                    .HasMaxLength(4);

                entity.Property(e => e.AccountUserLogin)
                    .HasColumnName("account_user_login")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Departments>(entity =>
            {
                entity.HasKey(e => e.DepartmentId)
                    .HasName("Departments_pkey");

                entity.ToTable("Departments", "inventory");

                entity.HasIndex(e => e.DepartmentRegionId)
                    .HasName("departments_department_region_id_idx");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("department_id")
                    .HasDefaultValueSql("nextval('inventory.dep_id_seq'::regclass)");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("department_name")
                    .HasMaxLength(255);

                entity.Property(e => e.DepartmentNotes)
                    .HasColumnName("department_notes")
                    .HasMaxLength(4000);

                entity.Property(e => e.DepartmentParentId).HasColumnName("department_parent_id");

                entity.Property(e => e.DepartmentRegionId).HasColumnName("department_region_id");

                entity.HasOne(d => d.DepartmentParent)
                    .WithMany(p => p.InverseDepartmentParent)
                    .HasForeignKey(d => d.DepartmentParentId)
                    .HasConstraintName("departments_departments_fk");

                entity.HasOne(d => d.DepartmentRegion)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.DepartmentRegionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("departments_region_fk");
            });

            modelBuilder.Entity<Employees>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("Employees_pkey");

                entity.ToTable("Employees", "inventory");

                entity.HasIndex(e => e.EmployeeFullFio)
                    .HasName("employees_employee_full_fio_idx");

                entity.HasIndex(e => e.EmployeeIsMol)
                    .HasName("employees_employee_is_mol_idx");

                entity.HasIndex(e => e.EmployeeIsRespons)
                    .HasName("employees_employee_is_respons_idx");

                entity.HasIndex(e => e.EmployeeLastname)
                    .HasName("employees_employee_lastname_idx");

                entity.HasIndex(e => e.EmployeeRegionId)
                    .HasName("employees_employee_region_id_idx");

                entity.Property(e => e.EmployeeId)
                    .HasColumnName("employee_id")
                    .HasDefaultValueSql("nextval('inventory.empl_id_seq'::regclass)");

                entity.Property(e => e.EmployeeEmail)
                    .HasColumnName("employee_email")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeFirstname)
                    .IsRequired()
                    .HasColumnName("employee_firstname")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeFullFio)
                    .HasColumnName("employee_full_fio")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeIsChief).HasColumnName("employee_is_chief");

                entity.Property(e => e.EmployeeIsMol).HasColumnName("employee_is_mol");

                entity.Property(e => e.EmployeeIsRespons).HasColumnName("employee_is_respons");

                entity.Property(e => e.EmployeeLastname)
                    .IsRequired()
                    .HasColumnName("employee_lastname")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeMiddlename)
                    .IsRequired()
                    .HasColumnName("employee_middlename")
                    .HasMaxLength(30);

                entity.Property(e => e.EmployeeNote)
                    .HasColumnName("employee_note")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeeOfficeId).HasColumnName("employee_office_id");

                entity.Property(e => e.EmployeePhoneHome)
                    .HasColumnName("employee_phone_home")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeePhoneWork)
                    .HasColumnName("employee_phone_work")
                    .HasMaxLength(255);

                entity.Property(e => e.EmployeePositionId).HasColumnName("employee_position_id");

                entity.Property(e => e.EmployeeRegionId)
                    .HasColumnName("employee_region_id")
                    .HasDefaultValueSql("54");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.EmployeeOffice)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_offices_fk");

                entity.HasOne(d => d.EmployeePosition)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeePositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_positions_fk");

                entity.HasOne(d => d.EmployeeRegion)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeRegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employees_region_fk");
            });

            modelBuilder.Entity<Houses>(entity =>
            {
                entity.ToTable("Houses", "inventory");

                entity.HasIndex(e => e.HousesName)
                    .HasName("houses_houses_name_idx");

                entity.HasIndex(e => e.HousesRegionId)
                    .HasName("fki_Houses_Region_id");

                entity.Property(e => e.HousesId)
                    .HasColumnName("houses_id")
                    .HasDefaultValueSql("nextval('inventory.house_id_seq'::regclass)");

                entity.Property(e => e.HousesName)
                    .HasColumnName("houses_name")
                    .HasMaxLength(255);

                entity.Property(e => e.HousesRegionId).HasColumnName("houses_region_id");

                entity.Property(e => e.HousesRem)
                    .HasColumnName("houses_rem")
                    .HasMaxLength(255);

                entity.HasOne(d => d.HousesRegion)
                    .WithMany(p => p.Houses)
                    .HasForeignKey(d => d.HousesRegionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("houses_region_fk");
            });

            modelBuilder.Entity<Offices>(entity =>
            {
                entity.HasKey(e => e.OfficeId)
                    .HasName("Offices_pkey");

                entity.ToTable("Offices", "inventory");

                entity.HasIndex(e => e.OfficeName)
                    .HasName("offices_office_name_idx");

                entity.Property(e => e.OfficeId)
                    .HasColumnName("office_id")
                    .HasDefaultValueSql("nextval('inventory.office_id_seq'::regclass)");

                entity.Property(e => e.OfficeHousesId).HasColumnName("office_houses_id");

                entity.Property(e => e.OfficeIsStore).HasColumnName("office_is_store");

                entity.Property(e => e.OfficeName)
                    .IsRequired()
                    .HasColumnName("office_name")
                    .HasMaxLength(255);

                entity.Property(e => e.OfficeNotes)
                    .HasColumnName("office_notes")
                    .HasMaxLength(4000);

                entity.HasOne(d => d.OfficeHouses)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.OfficeHousesId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("offices_houses_fk");
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.HasKey(e => e.PositionId)
                    .HasName("Positions_pkey");

                entity.ToTable("Positions", "inventory");

                entity.HasIndex(e => e.PositionName)
                    .HasName("positions_position_name_idx");

                entity.Property(e => e.PositionId)
                    .HasColumnName("position_id")
                    .HasDefaultValueSql("nextval('inventory.pos_id_seq'::regclass)");

                entity.Property(e => e.PositionDepartmentId).HasColumnName("position_department_id");

                entity.Property(e => e.PositionName)
                    .HasColumnName("position_name")
                    .HasMaxLength(255);

                entity.Property(e => e.PositionNotes)
                    .HasColumnName("position_notes")
                    .HasMaxLength(255);

                entity.HasOne(d => d.PositionDepartment)
                    .WithMany(p => p.Positions)
                    .HasForeignKey(d => d.PositionDepartmentId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("positions_departments_fk");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region", "inventory");

                entity.Property(e => e.RegionId)
                    .HasColumnName("region_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasColumnName("region_name")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<RelHardwareEmployee>(entity =>
            {
                entity.HasKey(e => e.RelheId)
                    .HasName("Rel_Hardware_Employee_pkey");

                entity.ToTable("Rel_Hardware_Employee", "inventory");

                entity.HasIndex(e => e.RelheEmployeeId)
                    .HasName("rel_hardware_employee_relhe_employee_id_idx");

                entity.HasIndex(e => e.RelheWhardId)
                    .HasName("rel_hardware_employee_relhe_whard_id_idx");

                entity.Property(e => e.RelheId)
                    .HasColumnName("relhe_id")
                    .HasDefaultValueSql("nextval('inventory.rel_hard_empl_id_seq'::regclass)");

                entity.Property(e => e.RelheEmployeeId).HasColumnName("relhe_employee_id");

                entity.Property(e => e.RelheWhardId).HasColumnName("relhe_whard_id");

                entity.HasOne(d => d.RelheEmployee)
                    .WithMany(p => p.RelHardwareEmployee)
                    .HasForeignKey(d => d.RelheEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_hardware_employee_employees_fk");

                entity.HasOne(d => d.RelheWhard)
                    .WithOne(p => p.RelHardwareEmployee)
                    .HasForeignKey<RelHardwareEmployee>(d => d.RelheWhardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_hardware_employee_wealth_hardware_fk");
            });

            modelBuilder.Entity<RelOfficeResponsEmployee>(entity =>
            {
                entity.HasKey(e => e.RoeId)
                    .HasName("Rel_Office_Respons_Employee_pkey");

                entity.ToTable("Rel_Office_Respons_Employee", "inventory");

                entity.HasIndex(e => e.RoeEmployeeId)
                    .HasName("rel_office_respons_employee_roe_employee_id_idx");

                entity.HasIndex(e => e.RoeOfficeId)
                    .HasName("rel_office_respons_employee_roe_office_id_idx");

                entity.Property(e => e.RoeId)
                    .HasColumnName("roe_id")
                    .HasDefaultValueSql("nextval('inventory.rel_office_resp_seq'::regclass)");

                entity.Property(e => e.RoeEmployeeId).HasColumnName("roe_employee_id");

                entity.Property(e => e.RoeOfficeId).HasColumnName("roe_office_id");

                entity.HasOne(d => d.RoeEmployee)
                    .WithMany(p => p.RelOfficeResponsEmployee)
                    .HasForeignKey(d => d.RoeEmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_office_respons_employee_employees_fk");

                entity.HasOne(d => d.RoeOffice)
                    .WithMany(p => p.RelOfficeResponsEmployee)
                    .HasForeignKey(d => d.RoeOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_office_respons_employee_offices_fk");
            });

            modelBuilder.Entity<RelSoftwareHardware>(entity =>
            {
                entity.HasKey(e => e.RelshId)
                    .HasName("Rel_Software_Hardware_pkey");

                entity.ToTable("Rel_Software_Hardware", "inventory");

                entity.HasIndex(e => e.RelshWhardId)
                    .HasName("rel_software_hardware_relsh_whard_id_idx");

                entity.HasIndex(e => e.RelshWsoftId)
                    .HasName("rel_software_hardware_relsh_wsoft_id_idx");

                entity.Property(e => e.RelshId)
                    .HasColumnName("relsh_id")
                    .HasDefaultValueSql("nextval('inventory.rel_soft_hard_seq'::regclass)");

                entity.Property(e => e.RelshWhardId).HasColumnName("relsh_whard_id");

                entity.Property(e => e.RelshWsoftId).HasColumnName("relsh_wsoft_id");

                entity.HasOne(d => d.RelshWhard)
                    .WithMany(p => p.RelSoftwareHardware)
                    .HasForeignKey(d => d.RelshWhardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_software_hardware_wealth_hardware_fk");

                entity.HasOne(d => d.RelshWsoft)
                    .WithMany(p => p.RelSoftwareHardware)
                    .HasForeignKey(d => d.RelshWsoftId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("rel_software_hardware_wealth_software_fk");
            });

            modelBuilder.Entity<WealthCategories>(entity =>
            {
                entity.HasKey(e => e.WcatId)
                    .HasName("Wealth_Categories_pkey");

                entity.ToTable("Wealth_Categories", "inventory");

                entity.Property(e => e.WcatId)
                    .HasColumnName("wcat_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Wcatname)
                    .IsRequired()
                    .HasColumnName("wcatname")
                    .HasMaxLength(255);

                entity.Property(e => e.Wcatnotes)
                    .HasColumnName("wcatnotes")
                    .HasMaxLength(4000);
            });

            modelBuilder.Entity<WealthHardware>(entity =>
            {
                entity.HasKey(e => e.WhardId)
                    .HasName("Wealth_Hardware_pkey");

                entity.ToTable("Wealth_Hardware", "inventory");

                entity.HasIndex(e => e.WhardFnumber)
                    .HasName("wealth_hardware_whard_fnumber_idx");

                entity.HasIndex(e => e.WhardInumber)
                    .HasName("wealth_hardware_whard_inumber_idx");

                entity.HasIndex(e => e.WhardName)
                    .HasName("wealth_hardware_whard_name_idx");

                entity.HasIndex(e => e.WhardRegionId)
                    .HasName("wealth_hardware_whard_region_id_idx");

                entity.HasIndex(e => e.WhardWcatId)
                    .HasName("wealth_hardware_whard_wcat_id_idx");

                entity.HasIndex(e => e.WhardWtypeId)
                    .HasName("wealth_hardware_whard_wtype_id_idx");

                entity.Property(e => e.WhardId)
                    .HasColumnName("whard_id")
                    .HasDefaultValueSql("nextval('inventory.whard_id_seq'::regclass)");

                entity.Property(e => e.IsSoftDeployed).HasColumnName("is_soft_deployed");

                entity.Property(e => e.WhardArchiv)
                    .HasColumnName("whard_archiv")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.WhardCreateDate)
                    .HasColumnName("whard_create_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.WhardDateOfAdoption)
                    .HasColumnName("whard_date_of_adoption")
                    .HasColumnType("date");

                entity.Property(e => e.WhardFnumber)
                    .HasColumnName("whard_fnumber")
                    .HasMaxLength(65);

                entity.Property(e => e.WhardInitialCost)
                    .HasColumnName("whard_initial_cost")
                    .HasColumnType("numeric(18,2)");

                entity.Property(e => e.WhardInumber)
                    .HasColumnName("whard_inumber")
                    .HasMaxLength(30);

                entity.Property(e => e.WhardMolEmployeeId).HasColumnName("whard_mol_employee_id");

                entity.Property(e => e.WhardName)
                    .HasColumnName("whard_name")
                    .HasMaxLength(255);

                entity.Property(e => e.WhardNote)
                    .HasColumnName("whard_note")
                    .HasMaxLength(255);

                entity.Property(e => e.WhardOfficeId).HasColumnName("whard_office_id");

                entity.Property(e => e.WhardRegionId).HasColumnName("whard_region_id");

                entity.Property(e => e.WhardResidualValue)
                    .HasColumnName("whard_residual_value")
                    .HasColumnType("numeric(18,2)");

                entity.Property(e => e.WhardWcatId).HasColumnName("whard_wcat_id");

                entity.Property(e => e.WhardWtypeId).HasColumnName("whard_wtype_id");

                entity.HasOne(d => d.WhardMolEmployee)
                    .WithMany(p => p.WealthHardware)
                    .HasForeignKey(d => d.WhardMolEmployeeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("wealth_hardware_employees_fk");

                entity.HasOne(d => d.WhardOffice)
                    .WithMany(p => p.WealthHardware)
                    .HasForeignKey(d => d.WhardOfficeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wealth_hardware_offices_fk");

                entity.HasOne(d => d.WhardRegion)
                    .WithMany(p => p.WealthHardware)
                    .HasForeignKey(d => d.WhardRegionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("wealth_hardware_region_fk");

                entity.HasOne(d => d.WhardWcat)
                    .WithMany(p => p.WealthHardware)
                    .HasForeignKey(d => d.WhardWcatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wealth_hardware_wealth_categories_fk");

                entity.HasOne(d => d.WhardWtype)
                    .WithMany(p => p.WealthHardware)
                    .HasForeignKey(d => d.WhardWtypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("wealth_hardware_wealth_types_fk");
            });

            modelBuilder.Entity<WealthSoftware>(entity =>
            {
                entity.HasKey(e => e.WsoftId)
                    .HasName("Wealth_Software_pkey");

                entity.ToTable("Wealth_Software", "inventory");

                entity.Property(e => e.WsoftId)
                    .HasColumnName("wsoft_id")
                    .HasDefaultValueSql("nextval('inventory.wsoft_id_seq'::regclass)");

                entity.Property(e => e.WsoftArchiv)
                    .HasColumnName("wsoft_archiv")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.WsoftCnt)
                    .HasColumnName("wsoft_cnt")
                    .HasDefaultValueSql("1")
                    .ForNpgsqlHasComment("Количество лицензий");

                entity.Property(e => e.WsoftCreateDate)
                    .HasColumnName("wsoft_create_date")
                    .HasColumnType("date")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.WsoftDateOfAdoption)
                    .HasColumnName("wsoft_date_of_adoption")
                    .HasColumnType("date");

                entity.Property(e => e.WsoftFnumber)
                    .HasColumnName("wsoft_fnumber")
                    .HasMaxLength(65);

                entity.Property(e => e.WsoftInitialCost)
                    .HasColumnName("wsoft_initial_cost")
                    .HasColumnType("numeric(18,2)");

                entity.Property(e => e.WsoftInumber)
                    .HasColumnName("wsoft_inumber")
                    .HasMaxLength(30);

                entity.Property(e => e.WsoftName)
                    .IsRequired()
                    .HasColumnName("wsoft_name")
                    .HasMaxLength(255);

                entity.Property(e => e.WsoftNote)
                    .HasColumnName("wsoft_note")
                    .HasMaxLength(255);

                entity.Property(e => e.WsoftRegionId).HasColumnName("wsoft_region_id");

                entity.Property(e => e.WsoftResidualValue)
                    .HasColumnName("wsoft_residual_value")
                    .HasColumnType("numeric(18,2)");

                entity.Property(e => e.WsoftWcatId).HasColumnName("wsoft_wcat_id");

                entity.Property(e => e.WsoftWtypeId).HasColumnName("wsoft_wtype_id");

                entity.HasOne(d => d.WsoftRegion)
                    .WithMany(p => p.WealthSoftware)
                    .HasForeignKey(d => d.WsoftRegionId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("wealth_software_region_fk");

                entity.HasOne(d => d.WsoftWcat)
                    .WithMany(p => p.WealthSoftware)
                    .HasForeignKey(d => d.WsoftWcatId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("wealth_software_wealth_categories_fk");

                entity.HasOne(d => d.WsoftWtype)
                    .WithMany(p => p.WealthSoftware)
                    .HasForeignKey(d => d.WsoftWtypeId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("wealth_software_wealth_types_fk");
            });

            modelBuilder.Entity<WealthTypes>(entity =>
            {
                entity.HasKey(e => e.WtypeId)
                    .HasName("Wealth_Types_pkey");

                entity.ToTable("Wealth_Types", "inventory");

                entity.HasIndex(e => e.WtypeName)
                    .HasName("wealth_types_wtype_name_idx");

                entity.Property(e => e.WtypeId)
                    .HasColumnName("wtype_id")
                    .HasDefaultValueSql("nextval('inventory.wtype_id_seq'::regclass)");

                entity.Property(e => e.WtypeName)
                    .IsRequired()
                    .HasColumnName("wtype_name")
                    .HasMaxLength(64);

                entity.Property(e => e.WtypeNotes)
                    .HasColumnName("wtype_notes")
                    .HasMaxLength(2048);
            });

            modelBuilder.HasSequence("ab_id_seq");

            modelBuilder.HasSequence("ac_id_seq");

            modelBuilder.HasSequence("acc_id_seq");

            modelBuilder.HasSequence("ap_id_seq");

            modelBuilder.HasSequence("at_id_seq");

            modelBuilder.HasSequence("dep_id_seq");

            modelBuilder.HasSequence("empl_id_seq");

            modelBuilder.HasSequence("house_id_seq");

            modelBuilder.HasSequence("office_id_seq");

            modelBuilder.HasSequence("pos_id_seq");

            modelBuilder.HasSequence("rel_hard_empl_id_seq");

            modelBuilder.HasSequence("rel_office_resp_seq");

            modelBuilder.HasSequence("rel_soft_hard_seq");

            modelBuilder.HasSequence("whard_id_seq");

            modelBuilder.HasSequence("wsoft_id_seq");

            modelBuilder.HasSequence("wtype_id_seq");
        }
    }
}
