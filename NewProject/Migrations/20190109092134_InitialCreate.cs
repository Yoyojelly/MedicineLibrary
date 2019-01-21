using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewProject.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JGLBs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    jglbID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    lbName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JGLBs", x => x.jglbID);
                });

            migrationBuilder.CreateTable(
                name: "powerTeams",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    powerTeamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    powerTeamName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_powerTeams", x => x.powerTeamID);
                });

            migrationBuilder.CreateTable(
                name: "SPBLFYs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BLFYBH = table.Column<string>(nullable: true),
                    SPBH = table.Column<int>(nullable: false),
                    BLFYMC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPBLFYs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "SPGGs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GGBH = table.Column<string>(nullable: true),
                    SPBH = table.Column<int>(nullable: false),
                    GGMC = table.Column<string>(nullable: true),
                    BZSL = table.Column<string>(nullable: true),
                    BZDW = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPGGs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "SPJJZs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JJZBH = table.Column<string>(nullable: true),
                    SPBH = table.Column<int>(nullable: false),
                    JJZMC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPJJZs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "SPSYZs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SYZBH = table.Column<string>(nullable: true),
                    SPBH = table.Column<int>(nullable: false),
                    SYZMC = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPSYZs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "SPTSRQYYs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SPBH = table.Column<int>(nullable: false),
                    RQLX = table.Column<string>(nullable: true),
                    YYSM = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPTSRQYYs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "SPYBXXs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    YBBH = table.Column<string>(nullable: true),
                    SHENG = table.Column<string>(nullable: true),
                    SHI = table.Column<string>(nullable: true),
                    SPBH = table.Column<int>(nullable: false),
                    YBZT = table.Column<string>(nullable: true),
                    YBLB = table.Column<string>(nullable: true),
                    XXLY = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPYBXXs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "SPYWXHZYs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    GUID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    XHZYBH = table.Column<string>(nullable: true),
                    SPBH1 = table.Column<int>(nullable: false),
                    SPBH2 = table.Column<int>(nullable: false),
                    ZYXG = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPYWXHZYs", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "subPower",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    subPowerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    subPowerName = table.Column<string>(nullable: true),
                    powerUrl = table.Column<string>(nullable: true),
                    supPowerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subPower", x => x.subPowerID);
                });

            migrationBuilder.CreateTable(
                name: "TYMD",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    DLID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GUID = table.Column<string>(nullable: true),
                    DNAME = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYMD", x => x.DLID);
                });

            migrationBuilder.CreateTable(
                name: "TYMLBs",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    LBID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GUID = table.Column<string>(nullable: true),
                    LBNAME = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYMLBs", x => x.LBID);
                });

            migrationBuilder.CreateTable(
                name: "TYMX",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    XLID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GUID = table.Column<string>(nullable: true),
                    XNAME = table.Column<string>(nullable: true),
                    ZLID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYMX", x => x.XLID);
                });

            migrationBuilder.CreateTable(
                name: "TYMZ",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    ZLID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GUID = table.Column<string>(nullable: true),
                    ZNAME = table.Column<string>(nullable: true),
                    DLID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TYMZ", x => x.ZLID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    user_code = table.Column<string>(nullable: false),
                    user_name = table.Column<string>(nullable: false),
                    user_pwd = table.Column<string>(nullable: false),
                    isDisable = table.Column<string>(nullable: true),
                    zzjg_code = table.Column<string>(nullable: true),
                    user_explain = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "TeamPowers",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    TeamPowersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    powerTeamID = table.Column<int>(nullable: false),
                    subPowerID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamPowers", x => x.TeamPowersID);
                    table.ForeignKey(
                        name: "FK_TeamPowers_powerTeams_powerTeamID",
                        column: x => x.powerTeamID,
                        principalTable: "powerTeams",
                        principalColumn: "powerTeamID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeamPowers_subPower_subPowerID",
                        column: x => x.subPowerID,
                        principalTable: "subPower",
                        principalColumn: "subPowerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicine",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    MedicineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MedicineName = table.Column<string>(nullable: true),
                    bzctID = table.Column<int>(nullable: false),
                    DLID = table.Column<int>(nullable: false),
                    ZLID = table.Column<int>(nullable: false),
                    XLID = table.Column<int>(nullable: false),
                    LBID = table.Column<int>(nullable: false),
                    jglbID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicine", x => x.MedicineID);
                    table.ForeignKey(
                        name: "FK_Medicine_TYMD_DLID",
                        column: x => x.DLID,
                        principalTable: "TYMD",
                        principalColumn: "DLID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicine_TYMLBs_LBID",
                        column: x => x.LBID,
                        principalTable: "TYMLBs",
                        principalColumn: "LBID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicine_TYMX_XLID",
                        column: x => x.XLID,
                        principalTable: "TYMX",
                        principalColumn: "XLID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicine_TYMZ_ZLID",
                        column: x => x.ZLID,
                        principalTable: "TYMZ",
                        principalColumn: "ZLID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medicine_JGLBs_jglbID",
                        column: x => x.jglbID,
                        principalTable: "JGLBs",
                        principalColumn: "jglbID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userPowers",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    UserPowersID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserID = table.Column<int>(nullable: false),
                    powerTeamID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userPowers", x => x.UserPowersID);
                    table.ForeignKey(
                        name: "FK_userPowers_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_userPowers_powerTeams_powerTeamID",
                        column: x => x.powerTeamID,
                        principalTable: "powerTeams",
                        principalColumn: "powerTeamID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SPZSJ",
                columns: table => new
                {
                    OperatorID = table.Column<int>(nullable: false),
                    OperatorTime = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false),
                    SPBH = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PZWH = table.Column<string>(nullable: true),
                    YSPM = table.Column<string>(nullable: true),
                    MedicineID = table.Column<int>(nullable: false),
                    JX = table.Column<string>(nullable: true),
                    SPM = table.Column<string>(nullable: true),
                    SCDW = table.Column<string>(nullable: true),
                    JGLB = table.Column<string>(nullable: true),
                    YWLB = table.Column<string>(nullable: true),
                    SMSSYZ = table.Column<string>(nullable: true),
                    SMSYFYL = table.Column<string>(nullable: true),
                    FYYJS = table.Column<string>(nullable: true),
                    SMSJJZ = table.Column<string>(nullable: true),
                    BLFY = table.Column<string>(nullable: true),
                    YWXHZY = table.Column<string>(nullable: true),
                    XXLY = table.Column<string>(nullable: true),
                    YWCF = table.Column<string>(nullable: true),
                    TSRQYY = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SPZSJ", x => x.SPBH);
                    table.ForeignKey(
                        name: "FK_SPZSJ_Medicine_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicine",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "JGLBs",
                columns: new[] { "jglbID", "OperatorID", "OperatorTime", "Version", "lbName" },
                values: new object[,]
                {
                    { 1, 0, null, 0, "处方药" },
                    { 2, 0, null, 0, "非处方药" }
                });

            migrationBuilder.InsertData(
                table: "TYMD",
                columns: new[] { "DLID", "DNAME", "GUID", "OperatorID", "OperatorTime", "Version" },
                values: new object[,]
                {
                    { 3, "其他", null, 0, null, 0 },
                    { 2, "西药", null, 0, null, 0 },
                    { 1, "中药", null, 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "TYMLBs",
                columns: new[] { "LBID", "GUID", "LBNAME", "OperatorID", "OperatorTime", "Version" },
                values: new object[,]
                {
                    { 2, null, "类别2", 0, null, 0 },
                    { 1, null, "类别1", 0, null, 0 },
                    { 3, null, "类别3", 0, null, 0 }
                });

            migrationBuilder.InsertData(
                table: "TYMX",
                columns: new[] { "XLID", "GUID", "OperatorID", "OperatorTime", "Version", "XNAME", "ZLID" },
                values: new object[,]
                {
                    { 4, null, 0, null, 0, "中药小类4", 2 },
                    { 6, null, 0, null, 0, "西药小类2", 3 },
                    { 7, null, 0, null, 0, "西药小类3", 4 },
                    { 8, null, 0, null, 0, "西药小类4", 4 },
                    { 9, null, 0, null, 0, "其他小类1", 5 },
                    { 10, null, 0, null, 0, "其他小类2", 5 },
                    { 11, null, 0, null, 0, "其他小类3", 6 },
                    { 12, null, 0, null, 0, "其他小类4", 6 },
                    { 5, null, 0, null, 0, "西药小类1", 3 },
                    { 3, null, 0, null, 0, "中药小类3", 2 },
                    { 1, null, 0, null, 0, "中药小类1", 1 },
                    { 2, null, 0, null, 0, "中药小类2", 1 }
                });

            migrationBuilder.InsertData(
                table: "TYMZ",
                columns: new[] { "ZLID", "DLID", "GUID", "OperatorID", "OperatorTime", "Version", "ZNAME" },
                values: new object[,]
                {
                    { 6, 3, null, 0, null, 0, "其他2" },
                    { 5, 3, null, 0, null, 0, "其他1" },
                    { 4, 2, null, 0, null, 0, "西药中类2" },
                    { 3, 2, null, 0, null, 0, "西药中类1" },
                    { 2, 1, null, 0, null, 0, "中药中类2" },
                    { 1, 1, null, 0, null, 0, "中药中类1" }
                });

            migrationBuilder.InsertData(
                table: "powerTeams",
                columns: new[] { "powerTeamID", "OperatorID", "OperatorTime", "Version", "powerTeamName" },
                values: new object[] { 1, 0, null, 0, "系统内置管理员" });

            migrationBuilder.InsertData(
                table: "subPower",
                columns: new[] { "subPowerID", "OperatorID", "OperatorTime", "Version", "powerUrl", "subPowerName", "supPowerID" },
                values: new object[,]
                {
                    { 1, 0, null, 0, "", "模块", 0 },
                    { 3, 0, null, 0, "", "系统设置", 1 },
                    { 4, 0, null, 0, "yhgl", "用户管理", 3 },
                    { 5, 0, null, 0, "", "系统设置", 3 },
                    { 6, 0, null, 0, "cdgl", "菜单管理", 5 },
                    { 7, 0, null, 0, "qxzgl", "权限组管理", 5 },
                    { 8, 0, null, 0, "qxcd", "权限菜单维护", 5 },
                    { 2, 0, null, 0, "tymk", "通用名库", 1 }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "UserID", "OperatorID", "OperatorTime", "Version", "isDisable", "user_code", "user_explain", "user_name", "user_pwd", "zzjg_code" },
                values: new object[] { 1, 0, null, 0, "0", "Administrator", "系统内置管理员", "管理员", "123456", "001" });

            migrationBuilder.InsertData(
                table: "TeamPowers",
                columns: new[] { "TeamPowersID", "OperatorID", "OperatorTime", "Version", "powerTeamID", "subPowerID" },
                values: new object[,]
                {
                    { 1, 0, null, 0, 1, 1 },
                    { 2, 0, null, 0, 1, 2 },
                    { 3, 0, null, 0, 1, 3 },
                    { 4, 0, null, 0, 1, 4 },
                    { 5, 0, null, 0, 1, 5 },
                    { 6, 0, null, 0, 1, 6 },
                    { 7, 0, null, 0, 1, 7 },
                    { 8, 0, null, 0, 1, 8 }
                });

            migrationBuilder.InsertData(
                table: "userPowers",
                columns: new[] { "UserPowersID", "OperatorID", "OperatorTime", "UserID", "Version", "powerTeamID" },
                values: new object[] { 1, 0, null, 1, 0, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_DLID",
                table: "Medicine",
                column: "DLID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_LBID",
                table: "Medicine",
                column: "LBID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_XLID",
                table: "Medicine",
                column: "XLID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_ZLID",
                table: "Medicine",
                column: "ZLID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_jglbID",
                table: "Medicine",
                column: "jglbID");

            migrationBuilder.CreateIndex(
                name: "IX_SPZSJ_MedicineID",
                table: "SPZSJ",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPowers_powerTeamID",
                table: "TeamPowers",
                column: "powerTeamID");

            migrationBuilder.CreateIndex(
                name: "IX_TeamPowers_subPowerID",
                table: "TeamPowers",
                column: "subPowerID");

            migrationBuilder.CreateIndex(
                name: "IX_userPowers_UserID",
                table: "userPowers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_userPowers_powerTeamID",
                table: "userPowers",
                column: "powerTeamID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SPBLFYs");

            migrationBuilder.DropTable(
                name: "SPGGs");

            migrationBuilder.DropTable(
                name: "SPJJZs");

            migrationBuilder.DropTable(
                name: "SPSYZs");

            migrationBuilder.DropTable(
                name: "SPTSRQYYs");

            migrationBuilder.DropTable(
                name: "SPYBXXs");

            migrationBuilder.DropTable(
                name: "SPYWXHZYs");

            migrationBuilder.DropTable(
                name: "SPZSJ");

            migrationBuilder.DropTable(
                name: "TeamPowers");

            migrationBuilder.DropTable(
                name: "userPowers");

            migrationBuilder.DropTable(
                name: "Medicine");

            migrationBuilder.DropTable(
                name: "subPower");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "powerTeams");

            migrationBuilder.DropTable(
                name: "TYMD");

            migrationBuilder.DropTable(
                name: "TYMLBs");

            migrationBuilder.DropTable(
                name: "TYMX");

            migrationBuilder.DropTable(
                name: "TYMZ");

            migrationBuilder.DropTable(
                name: "JGLBs");
        }
    }
}
