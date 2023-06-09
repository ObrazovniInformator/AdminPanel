using AdminPanel.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Data
{
    public class AdminPanelContext : IdentityDbContext<IdentityUser>
    {
        public AdminPanelContext() => Database.SetCommandTimeout(150000);

        public AdminPanelContext(DbContextOptions<AdminPanelContext> options)
            : base(options)
        {
        }
        public virtual DbSet<GlavneOblasti> GlavneOblasti { get; set; }
        public virtual DbSet<Rubrika> Rubrika { get; set; }
        public virtual DbSet<Podrubrika> Podrubrika { get; set; }
        public virtual DbSet<Propis> Propis { get; set; }
        public virtual DbSet<Pretplatnik> Pretplatnik { get; set; }
        public virtual DbSet<NivoPodnaslova> NivoPodnaslova { get; set; }
        public virtual DbSet<Podnaslov> Podnaslov { get; set; }
        public virtual DbSet<Clan> Clan { get; set; }
        public virtual DbSet<Stav> Stav { get; set; }
        public virtual DbSet<Tacka> Tacka { get; set; }
        public virtual DbSet<Alineja> Alineja { get; set; }
        public virtual DbSet<SudskaPraksa> SudskaPraksa { get; set; }
        public virtual DbSet<RubrikaSP> RubrikaSP { get; set; }
        public virtual DbSet<PodrubrikaSP> PodrubrikaSP { get; set; }
        //public virtual DbSet<PodpodrubrikaSP> PodpodrubrikaSP { get; set; }
        public virtual DbSet<DonosilacSP> DonosilacSP { get; set; }
        public virtual DbSet<PropisSudskaPraksa> PropisSudskaPraksa { get; set; }
        public virtual DbSet<SluzbenoMisljenje> SluzbenoMisljenje { get; set; }
        public virtual DbSet<RubrikaSM> RubrikaSM { get; set; }
        public virtual DbSet<PodrubrikaSM> PodrubrikaSM { get; set; }
        //public virtual DbSet<PodpodrubrikaSM> PodpodrubrikaSM { get; set; }
        public virtual DbSet<DonosilacSM> DonosilacSM { get; set; }
        public virtual DbSet<PropisSluzbenoMisljenje> PropisSluzbenoMisljenje { get; set; }
        //CASOPIS
        public virtual DbSet<CasopisOznaka> CasopisOznaka { get; set; }
        public virtual DbSet<CasopisGodina> CasopisGodina { get; set; }
        public virtual DbSet<CasopisBroj> CasopisBroj { get; set; }
        public virtual DbSet<RubrikaCasopis> RubrikaCasopis { get; set; }
        public virtual DbSet<PodrubrikaCasopis> PodrubrikaCasopis { get; set; }
        public virtual DbSet<PropisCasopis> PropisCasopis { get; set; }
        public virtual DbSet<GlavneOblastiCasopis> GlavneOblastiCasopis { get; set; }
        public virtual DbSet<CasopisNaslov> CasopisNaslov { get; set; }
        public virtual DbSet<PdfFajCasopis> PdfFajlCasopis { get; set; }

        // public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<RubrikaVesti> RubrikaVesti { get; set; }
        public virtual DbSet<Vest> Vest { get; set; }
        public virtual DbSet<InAkta> InAkta { get; set; }
        public virtual DbSet<InAktaPodvrsta> InAktaPodvrsta { get; set; }
        public virtual DbSet<RubrikaInAkta> RubrikaInAkta { get; set; }
        public virtual DbSet<PodrubrikaInAkta> PodrubrikaInAkta { get; set; }
        public virtual DbSet<PodpodrubrikaInAkta> PodpodrubrikaInAkta { get; set; }
        public virtual DbSet<PodpodpodrubrikaInAkta> PodpodpodrubrikaInAkta { get; set; }
        public virtual DbSet<PodpodpodpodrubrikaInAkta> PodpodpodpodrubrikaInAkta { get; set; }
        public virtual DbSet<PropisInAkta> PropisInAkta { get; set; }
        //PROSVETNI PROPISI:
        public virtual DbSet<RubrikaPP> RubrikaPP { get; set; }
        public virtual DbSet<PodrubrikaPP> PodrubrikaPP { get; set; }
        public virtual DbSet<ProsvetniPropis> ProsvetnIPropis { get; set; }
        public virtual DbSet<PodnaslovPP> PodnaslovPP { get; set; }
        public virtual DbSet<ClanPP> ClanPP { get; set; }
        public virtual DbSet<StavPP> StavPP { get; set; }
        public virtual DbSet<TackaPP> TackaPP { get; set; }
        public virtual DbSet<AlinejaPP> AlinejaPP { get; set; }
        public virtual DbSet<ProsvetniPropisInAkta> ProsvetniPropisInAkta { get; set; }
        public virtual DbSet<ProsvetniPropisSluzbenoMisljenje> ProsvetniPropisSluzbenoMisljenje { get; set; }
        public virtual DbSet<ProsvetniPropisSudskaPraksa> ProsvetniPropisSudskaPraksa { get; set; }
        public virtual DbSet<PdfFajlPropis> PdfFajlPropis { get; set; }
        public virtual DbSet<PdfFajlProsvetniPropis> PdfFajlProsvetniPropis { get; set; }
        //primeri knjizenja
        public virtual DbSet<PrimeriKnjizenja> PrimeriKnjizenja { get; set; }
        public virtual DbSet<RubrikaPK> RubrikaPK { get; set; }
        public virtual DbSet<PropisPrimeriKnjizenja> PropisPrimeriKnjizenja { get; set; }
        public virtual DbSet<PropisVest> PropisVest { get; set; }
        public virtual DbSet<PropisPropis> PropisPropis { get; set; }
        public virtual DbSet<VestiKategorija> VestiKategorija { get; set; }

        //Greske
        public virtual DbSet<PracenjeGresaka> PracenjeGresaka { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=DESKTOP-J6TSTOI\\SQLEXPRESS;Database=adminPanel;Integrated Security=True;");
                optionsBuilder.UseSqlServer("Server=tcp:obrazovni-adminpanel.database.windows.net,1433;Initial Catalog=adminPanel;Persist Security Info=False;User ID=Andjelija;Password=Andja#Luka;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Podrubrika>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.Podrubrika)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_Podrubrika_Rubrika");
            });
            modelBuilder.Entity<Propis>(entity =>
            {
                entity.HasOne(d => d.IdPodrubrikaNavigation)
                       .WithMany(p => p.Propis)
                       .HasForeignKey(d => d.IdPodrubrike)
                       .HasConstraintName("FK_Propis_Podrubrika");

                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.Propis)
                    .HasForeignKey(d => d.IdRubrike)
                    .HasConstraintName("FK_Propis_Rubrika");
            });
            modelBuilder.Entity<Podnaslov>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.Podnaslov)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_Podnaslov_Propis");

                entity.HasOne(d => d.IdNivoPodnaslovaNavigation)
                   .WithMany(p => p.Podnaslov)
                   .HasForeignKey(d => d.IdNivoPodnaslova)
                   .HasConstraintName("FK_Podnaslov_NivoPodnaslova");

            });
            modelBuilder.Entity<Clan>(entity =>
            {
                entity.HasOne(d => d.IdPodnaslovNavigation)
                       .WithMany(p => p.Clan)
                       .HasForeignKey(d => d.IdPodnaslov)
                       .HasConstraintName("FK_Clan_Podnaslov");
                entity.HasOne(d => d.IdPropisNavigation)
                       .WithMany(p => p.Clan)
                       .HasForeignKey(d => d.IdPropis)
                       .HasConstraintName("FK_Clan_Propis");
            });
            modelBuilder.Entity<Stav>(entity =>
            {
                entity.HasOne(d => d.IdClanNavigation)
                       .WithMany(p => p.Stav)
                       .HasForeignKey(d => d.IdClan)
                       .HasConstraintName("FK_Stav_Clan");
            });
            modelBuilder.Entity<Tacka>(entity =>
            {
                entity.HasOne(d => d.IdStavNavigation)
                       .WithMany(p => p.Tacka)
                       .HasForeignKey(d => d.IdStav)
                       .HasConstraintName("FK_Tacka_Stav");
            });
            modelBuilder.Entity<Alineja>(entity =>
            {
                entity.HasOne(d => d.IdStavNavigation)
                       .WithMany(p => p.Alineja)
                       .HasForeignKey(d => d.IdStav)
                       .HasConstraintName("FK_Alineja_Stav");
            });
            modelBuilder.Entity<SudskaPraksa>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.SudskaPraksa)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_SudskaPraksa_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                   .WithMany(p => p.SudskaPraksa)
                   .HasForeignKey(d => d.IdClan)
                   .HasConstraintName("FK_SudskaPraksa_Clan");

                entity.HasOne(d => d.IdStavNavigation)
                   .WithMany(p => p.SudskaPraksa)
                   .HasForeignKey(d => d.IdStav)
                   .HasConstraintName("FK_SudskaPraksa_Stav");

                entity.HasOne(d => d.IdTackaNavigation)
                   .WithMany(p => p.SudskaPraksa)
                   .HasForeignKey(d => d.IdTacka)
                   .HasConstraintName("FK_SudskaPraksa_Tacka");

                entity.HasOne(d => d.IdRubrikaSPNavigation)
                    .WithMany(p => p.SudskaPraksa)
                    .HasForeignKey(d => d.IdRubrikaSP)
                    .HasConstraintName("FK_SudskaPraksa_RubrikaSP");

                entity.HasOne(d => d.IdPodrubrikaSPNavigation)
                   .WithMany(p => p.SudskaPraksa)
                   .HasForeignKey(d => d.IdPodrubrikaSP)
                   .HasConstraintName("FK_SudskaPraksa_PodrubrikaSP");

                //entity.HasOne(d => d.IdPodpodrubrikaSPNavigation)
                //      .WithMany(p => p.SudskaPraksa)
                //      .HasForeignKey(d => d.IdPodpodrubrikaSP)
                //      .HasConstraintName("FK_SudskaPraksa_PodpodrubrikaSP");

                entity.HasOne(d => d.IdDonosilacSPNavigation)
                     .WithMany(p => p.SudskaPraksa)
                     .HasForeignKey(d => d.IdDonosilacSP)
                     .HasConstraintName("FK_SudskaPraksa_DonosilacSP");
            });

            modelBuilder.Entity<PodrubrikaSP>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.PodrubrikaSP)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_PodrubrikaSP_RubrikaSP");
            });

            modelBuilder.Entity<SluzbenoMisljenje>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.SluzbenoMisljenje)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_SluzbenoMisljenje_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                   .WithMany(p => p.SluzbenoMisljenje)
                   .HasForeignKey(d => d.IdClan)
                   .HasConstraintName("FK_SluzbenoMisljenje_Clan");

                entity.HasOne(d => d.IdStavNavigation)
                   .WithMany(p => p.SluzbenoMisljenje)
                   .HasForeignKey(d => d.IdStav)
                   .HasConstraintName("FK_SluzbenoMisljenje_Stav");

                entity.HasOne(d => d.IdTackaNavigation)
                   .WithMany(p => p.SluzbenoMisljenje)
                   .HasForeignKey(d => d.IdTacka)
                   .HasConstraintName("FK_SluzbenoMisljenje_Tacka");

                entity.HasOne(d => d.IdRubrikaSMNavigation)
                    .WithMany(p => p.SluzbenoMisljenje)
                    .HasForeignKey(d => d.IdRubrikaSM)
                    .HasConstraintName("FK_SluzbenaMisljenja_RubrikaSM");

                entity.HasOne(d => d.IdPodrubrikaSMNavigation)
                   .WithMany(p => p.SluzbenoMisljenje)
                   .HasForeignKey(d => d.IdPodrubrikaSM)
                   .HasConstraintName("FK_SluzbenaMisljenja_PodrubrikaSM");

                //entity.HasOne(d => d.IdPodpodrubrikaSMNavigation)
                //      .WithMany(p => p.SluzbenoMisljenje)
                //      .HasForeignKey(d => d.IdPodpodrubrikaSM)
                //      .HasConstraintName("FK_SluzbenoMisljenje_PodpodrubrikaSM");

                entity.HasOne(d => d.IdDonosilacSMNavigation)
                     .WithMany(p => p.SluzbenoMisljenje)
                     .HasForeignKey(d => d.IdDonosilacSM)
                     .HasConstraintName("FK_SluzbenaMisljenja_DonosilacSM");
            });

            modelBuilder.Entity<PodrubrikaSM>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.PodrubrikaSM)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_PodrubrikaSM_RubrikaSM");
            });

            modelBuilder.Entity<CasopisNaslov>(entity =>
            {
                entity.HasOne(d => d.IdPodrubrikaCasopisNavigation)
                    .WithMany(p => p.CasopisNaslov)
                    .HasForeignKey(d => d.IdPodrubrika)
                    .HasConstraintName("FK_CasopisNaslov_PodrubrikaCasopis");

                entity.HasOne(d => d.IdRubrikaCasopisNavigation)
                    .WithMany(p => p.CasopisNaslov)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_CasopisNaslov_RubrikaCasopis");

                entity.HasOne(d => d.IdCasopisBrojNavigation)
                   .WithMany(p => p.CasopisNaslov)
                   .HasForeignKey(d => d.IdBroj)
                   .HasConstraintName("FK_CasopisNaslov_CasopisBroj");

                entity.HasOne(d => d.IdCasopisGodinaNavigation)
                   .WithMany(p => p.CasopisNaslov)
                   .HasForeignKey(d => d.IdGodina)
                   .HasConstraintName("FK_CasopisNaslov_CasopisGodina");

                entity.HasOne(d => d.IdCasopisOznakaNavigation)
                   .WithMany(p => p.CasopisNaslov)
                   .HasForeignKey(d => d.IdOznaka)
                   .HasConstraintName("FK_CasopisNaslov_CasopisOznaka");
            });

            modelBuilder.Entity<CasopisBroj>(entity =>
            {
                entity.HasOne(d => d.IdCasopisGodinaNavigation)
                    .WithMany(p => p.CasopisBroj)
                    .HasForeignKey(d => d.IdGodina)
                    .HasConstraintName("FK_CasopisBroj_CasopisGodina");
            });

            modelBuilder.Entity<CasopisGodina>(entity =>
            {
                entity.HasOne(d => d.IdGlavneOblastiCasopisNavigation)
                    .WithMany(p => p.CasopisGodina)
                    .HasForeignKey(d => d.IdGlavneOblastiCasopis)
                    .HasConstraintName("FK_CasopisGodina_GlavneOblastiCasopis");
            });

            modelBuilder.Entity<RubrikaCasopis>(entity =>
            {
                entity.HasOne(d => d.IdBrojNavigation)
                    .WithMany(p => p.RubrikaCasopis)
                    .HasForeignKey(d => d.IdBroj)
                    .HasConstraintName("FK_RubrikaCasopis_CasopisBroj");
            });

            modelBuilder.Entity<PropisCasopis>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                    .WithMany(p => p.PropisCasopis)
                    .HasForeignKey(d => d.IdPropis)
                    .HasConstraintName("FK_PropisCasopis_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                    .WithMany(p => p.PropisCasopis)
                    .HasForeignKey(d => d.IdClan)
                    .HasConstraintName("FK_PropisCasopis_Clan");
                entity.HasOne(d => d.IdStavNavigation)
                    .WithMany(p => p.PropisCasopis)
                    .HasForeignKey(d => d.IdStav)
                    .HasConstraintName("FK_PropisCasopis_Stav");
                entity.HasOne(d => d.IdTackaNavigation)
                    .WithMany(p => p.PropisCasopis)
                    .HasForeignKey(d => d.IdTacka)
                    .HasConstraintName("FK_PropisCasopis_Tacka");
                entity.HasOne(d => d.IdCasopisNaslovNavigation)
                    .WithMany(p => p.PropisCasopis)
                    .HasForeignKey(d => d.IdCasopis)
                    .HasConstraintName("FK_PropisCasopis_CasopisNaslov");

            });

            modelBuilder.Entity<PodrubrikaCasopis>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.PodrubrikaCasopis)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_PodrubrikaCasopis_RubrikaCasopis");
            });

            modelBuilder.Entity<Vest>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaVestiNavigation)
                    .WithMany(p => p.Vest)
                    .HasForeignKey(d => d.IdRubrikaVesti)
                    .HasConstraintName("FK_VEST_RUBRIKA");

                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.Vest)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_Vest_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                   .WithMany(p => p.Vest)
                   .HasForeignKey(d => d.IdClan)
                   .HasConstraintName("FK_Vest_Clan");

                 entity.HasOne(d => d.IdKategorijaNavigation)
                   .WithMany(p => p.Vest)
                   .HasForeignKey(d => d.IdKategorija)
                   .HasConstraintName("FK_Vest_Kategorija");

                entity.HasOne(d => d.IdStavNavigation)
                   .WithMany(p => p.Vest)
                   .HasForeignKey(d => d.IdStav)
                   .HasConstraintName("FK_Vest_Stav");

                entity.HasOne(d => d.IdTackaNavigation)
                   .WithMany(p => p.Vest)
                   .HasForeignKey(d => d.IdTacka)
                   .HasConstraintName("FK_Vest_Tacka");
            });

            modelBuilder.Entity<PodpodpodpodrubrikaInAkta>(entity =>
            {
                entity.HasOne(d => d.IdPodpodpodrubrikaInAktaNavigation)
                    .WithMany(p => p.PodpodpodpodrubrikaInAkta)
                    .HasForeignKey(d => d.IdPodpodpodrubrika)
                    .HasConstraintName("FK_PodpodpodpodrubrikaInAkta_PodpodpodrubrikaInAkta");
            });

            modelBuilder.Entity<PodpodpodrubrikaInAkta>(entity =>
            {
                entity.HasOne(d => d.IdPodpodrubrikaInAktaNavigation)
                    .WithMany(p => p.PodpodpodrubrikaInAkta)
                    .HasForeignKey(d => d.IdPodpodrubrika)
                    .HasConstraintName("FK_PodpodpodrubrikaInAkta_PodpodrubrikaInAkta");
            });

            modelBuilder.Entity<PodpodrubrikaInAkta>(entity =>
            {
                entity.HasOne(d => d.IdPodrubrikaInAktaNavigation)
                    .WithMany(p => p.PodpodrubrikaInAkta)
                    .HasForeignKey(d => d.IdPodrubrika)
                    .HasConstraintName("FK_PodpodrubrikaInAkta_PodrubrikaInAkta");
            });

            modelBuilder.Entity<PodrubrikaInAkta>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaInAktaNavigation)
                    .WithMany(p => p.PodrubrikaInAkta)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_PodrubrikaInAkta_RubrikaInAkta");
            });

            modelBuilder.Entity<RubrikaInAkta>(entity =>
            {
                entity.HasOne(d => d.IdInAktaPodvrstaNavigation)
                    .WithMany(p => p.RubrikaInAkta)
                    .HasForeignKey(d => d.IdPodvrsta)
                    .HasConstraintName("FK_RubrikaInAkta_InAktaPodvrsta");
            });

            modelBuilder.Entity<PropisInAkta>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                    .WithMany(p => p.PropisInAkta)
                    .HasForeignKey(d => d.IdPropis)
                    .HasConstraintName("FK_PropisInAkta_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                    .WithMany(p => p.PropisInAkta)
                    .HasForeignKey(d => d.IdClan)
                    .HasConstraintName("FK_PropisInAkta_Clan");
                entity.HasOne(d => d.IdStavNavigation)
                    .WithMany(p => p.PropisInAkta)
                    .HasForeignKey(d => d.IdStav)
                    .HasConstraintName("FK_PropisInAkta_Stav");
                entity.HasOne(d => d.IdTackaNavigation)
                    .WithMany(p => p.PropisInAkta)
                    .HasForeignKey(d => d.IdTacka)
                    .HasConstraintName("FK_PropisCasopis_Tacka");
                entity.HasOne(d => d.IdInAktaNavigation)
                    .WithMany(p => p.PropisInAkta)
                    .HasForeignKey(d => d.IdInAkta)
                    .HasConstraintName("FK_PropisInAkta_InAkta");
            });

            modelBuilder.Entity<PodrubrikaPP>(entity =>
            {
                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.PodrubrikaPP)
                    .HasForeignKey(d => d.IdRubrika)
                    .HasConstraintName("FK_PodrubrikaPP_RubrikaPP");
            });
            modelBuilder.Entity<ProsvetniPropis>(entity =>
            {
                entity.HasOne(d => d.IdPodrubrikaNavigation)
                       .WithMany(p => p.ProsvetniPropis)
                       .HasForeignKey(d => d.IdPodrubrike)
                       .HasConstraintName("FK_ProsvetniPropis_PodrubrikaPP");

                entity.HasOne(d => d.IdRubrikaNavigation)
                    .WithMany(p => p.ProsvetniPropis)
                    .HasForeignKey(d => d.IdRubrike)
                    .HasConstraintName("FK_Propis_Rubrika");
            });
            modelBuilder.Entity<PdfFajlPropis>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.PdfFajlPropis)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_PdfFajl_Propis");
            });
            modelBuilder.Entity<PdfFajlProsvetniPropis>(entity =>
            {
                entity.HasOne(d => d.IdProsvetniPropisNavigation)
                   .WithMany(p => p.PdfFajlProsvetniPropis)
                   .HasForeignKey(d => d.IdProsvetniPropis)
                   .HasConstraintName("FK_PdfFajl_ProsvetniPropis");
            });
            modelBuilder.Entity<PdfFajCasopis>(entity =>
            {
                entity.HasOne(d => d.IdCasopisNavigation)
                   .WithMany(p => p.PdfFajCasopis)
                   .HasForeignKey(d => d.IdCasopis)
                   .HasConstraintName("FK_PdfFajl_ProsvetniPropis");
            });
            modelBuilder.Entity<PodnaslovPP>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.PodnaslovPP)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_PodnaslovPP_ProsvetniPropis");

                entity.HasOne(d => d.IdNivoPodnaslovaNavigation)
                   .WithMany(p => p.PodnaslovPP)
                   .HasForeignKey(d => d.IdNivoPodnaslova)
                   .HasConstraintName("FK_PodnaslovPP_NivoPodnaslova");

            });
            modelBuilder.Entity<ClanPP>(entity =>
            {
                entity.HasOne(d => d.IdPodnaslovNavigation)
                       .WithMany(p => p.ClanPP)
                       .HasForeignKey(d => d.IdPodnaslov)
                       .HasConstraintName("FK_ClanPP_Podnaslov");
                entity.HasOne(d => d.IdPropisNavigation)
                       .WithMany(p => p.ClanPP)
                       .HasForeignKey(d => d.IdPropis)
                       .HasConstraintName("FK_ClanPP_Propis");
            });
            modelBuilder.Entity<StavPP>(entity =>
            {
                entity.HasOne(d => d.IdClanNavigation)
                       .WithMany(p => p.StavPP)
                       .HasForeignKey(d => d.IdClan)
                       .HasConstraintName("FK_StavPP_ClanPP");
            });
            modelBuilder.Entity<TackaPP>(entity =>
            {
                entity.HasOne(d => d.IdStavNavigation)
                       .WithMany(p => p.TackaPP)
                       .HasForeignKey(d => d.IdStav)
                       .HasConstraintName("FK_TackaPP_StavPP");
            });
            modelBuilder.Entity<AlinejaPP>(entity =>
            {
                entity.HasOne(d => d.IdStavNavigation)
                       .WithMany(p => p.AlinejaPP)
                       .HasForeignKey(d => d.IdStav)
                       .HasConstraintName("FK_AlinejaPP_StavPP");
            });
            modelBuilder.Entity<ProsvetniPropisInAkta>(entity =>
            {
                entity.HasOne(d => d.IdProsvetniPropisNavigation)
                    .WithMany(p => p.ProsvetniPropisInAkta)
                    .HasForeignKey(d => d.IdProsvetniPropis)
                    .HasConstraintName("FK_ProsvetniPropisInAkta_Propis");
                entity.HasOne(d => d.IdClanPPNavigation)
                    .WithMany(p => p.ProsvetniPropisInAkta)
                    .HasForeignKey(d => d.IdClanPP)
                    .HasConstraintName("FK_ProsvetniPropisInAkta_ClanPP");
                entity.HasOne(d => d.IdStavPPNavigation)
                    .WithMany(p => p.ProsvetniPropisInAkta)
                    .HasForeignKey(d => d.IdStavPP)
                    .HasConstraintName("FK_ProsvetniPropisInAkta_StavPP");
                entity.HasOne(d => d.IdTackaPPNavigation)
                    .WithMany(p => p.ProsvetniPropisInAkta)
                    .HasForeignKey(d => d.IdTackaPP)
                    .HasConstraintName("FK_ProsvetniPropisInAkta_TackaPP");
                entity.HasOne(d => d.IdInAktaNavigation)
                    .WithMany(p => p.ProsvetniPropisInAkta)
                    .HasForeignKey(d => d.IdInAkta)
                    .HasConstraintName("FK_ProsvetniPropisInAkta_InAktaPP");
            });
   
            modelBuilder.Entity<ProsvetniPropisSluzbenoMisljenje>(entity =>
            {
                entity.HasOne(d => d.IdProsvetniPropisNavigation)
                    .WithMany(p => p.ProsvetniPropisSluzbenoMisljenje)
                    .HasForeignKey(d => d.IdProsvetniPropis)
                    .HasConstraintName("FK_ProsvetniPropisSluzbenoMisljenje_Propis");
                entity.HasOne(d => d.IdClanPPNavigation)
                    .WithMany(p => p.ProsvetniPropisSluzbenoMisljenje)
                    .HasForeignKey(d => d.IdClanPP)
                    .HasConstraintName("FK_ProsvetniPropisSluzbenoMisljenje_ClanPP");
                entity.HasOne(d => d.IdStavPPNavigation)
                    .WithMany(p => p.ProsvetniPropisSluzbenoMisljenje)
                    .HasForeignKey(d => d.IdStavPP)
                    .HasConstraintName("FK_ProsvetniPropisSluzbenoMisljenje_StavPP");
                entity.HasOne(d => d.IdTackaPPNavigation)
                    .WithMany(p => p.ProsvetniPropisSluzbenoMisljenje)
                    .HasForeignKey(d => d.IdTackaPP)
                    .HasConstraintName("FK_ProsvetniPropisSluzbenoMisljenje_TackaPP");
                entity.HasOne(d => d.IdSluzbenoMisljenjeNavigation)
                    .WithMany(p => p.ProsvetniPropisSluzbenoMisljenje)
                    .HasForeignKey(d => d.IdSluzbenoMisljenje)
                    .HasConstraintName("FK_ProsvetniPropisSluzbenoMisljenje_SluzbenoMisljenjePP");
            });

            modelBuilder.Entity<ProsvetniPropisSudskaPraksa>(entity =>
            {
                entity.HasOne(d => d.IdProsvetniPropisNavigation)
                    .WithMany(p => p.ProsvetniPropisSudskaPraksa)
                    .HasForeignKey(d => d.IdProsvetniPropis)
                    .HasConstraintName("FK_ProsvetniPropisSudskaPraksa_Propis");
                entity.HasOne(d => d.IdClanPPNavigation)
                    .WithMany(p => p.ProsvetniPropisSudskaPraksa)
                    .HasForeignKey(d => d.IdClanPP)
                    .HasConstraintName("FK_ProsvetniPropisSudskaPraksa_ClanPP");
                entity.HasOne(d => d.IdStavPPNavigation)
                    .WithMany(p => p.ProsvetniPropisSudskaPraksa)
                    .HasForeignKey(d => d.IdStavPP)
                    .HasConstraintName("FK_ProsvetniPropisSudskaPraksa_StavPP");
                entity.HasOne(d => d.IdTackaPPNavigation)
                    .WithMany(p => p.ProsvetniPropisSudskaPraksa)
                    .HasForeignKey(d => d.IdTackaPP)
                    .HasConstraintName("FK_ProsvetniPropisSudskaPraksa_TackaPP");
                entity.HasOne(d => d.IdSudskaPraksaNavigation)
                    .WithMany(p => p.ProsvetniPropisSudskaPraksa)
                    .HasForeignKey(d => d.IdSudskaPraksa)
                    .HasConstraintName("FK_ProsvetniPropisSudskaPraksa_SudskaPraksaPP");
            });

            modelBuilder.Entity<PrimeriKnjizenja>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                   .WithMany(p => p.PrimeriKnjizenja)
                   .HasForeignKey(d => d.IdPropis)
                   .HasConstraintName("FK_PrimeriKnjizenja_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                   .WithMany(p => p.PrimeriKnjizenja)
                   .HasForeignKey(d => d.IdClan)
                   .HasConstraintName("FK_PrimeriKnjizenja_Clan");

                entity.HasOne(d => d.IdStavNavigation)
                   .WithMany(p => p.PrimeriKnjizenja)
                   .HasForeignKey(d => d.IdStav)
                   .HasConstraintName("FK_PrimeriKnjizenja_Stav");

                entity.HasOne(d => d.IdTackaNavigation)
                   .WithMany(p => p.PrimeriKnjizenja)
                   .HasForeignKey(d => d.IdTacka)
                   .HasConstraintName("FK_PrimeriKnjizenja_Tacka");

                entity.HasOne(d => d.IdRubrikaPKNavigation)
                    .WithMany(p => p.PrimeriKnjizenja)
                    .HasForeignKey(d => d.IdRubrikaPK)
                    .HasConstraintName("FK_PrimeriKnjizenja_RubrikaPK");
            });
            modelBuilder.Entity<PropisPropis>(entity =>
            {
                entity.HasOne(d => d.IdPropisNavigation)
                  .WithMany(p => p.PropisPropis)
                  .HasForeignKey(d => d.IdPropis)
                  .HasConstraintName("FK_PropisPropis_Propis");

                entity.HasOne(d => d.IdClanNavigation)
                   .WithMany(p => p.PropisPropis)
                   .HasForeignKey(d => d.IdClan)
                   .HasConstraintName("FK_PropisPropis_Clan");

                entity.HasOne(d => d.IdStavNavigation)
                   .WithMany(p => p.PropisPropis)
                   .HasForeignKey(d => d.IdStav)
                   .HasConstraintName("FK_PropisPropis_Stav");

                entity.HasOne(d => d.IdTackaNavigation)
                   .WithMany(p => p.PropisPropis)
                   .HasForeignKey(d => d.IdTacka)
                   .HasConstraintName("FK_PropisPropis_Tacka");

            });


            //modelBuilder.Entity<PodpodrubrikaSP>(entity =>
            //{
            //    entity.HasOne(d => d.IdPodrubrikaNavigation)
            //        .WithMany(p => p.PodpodrubrikaSP)
            //        .HasForeignKey(d => d.IdPodrubrika)
            //        .HasConstraintName("FK_PodpodrubrikaSP_PodrubrikaSP");
            //});

            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
