using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public partial class Xkom_ProjektContext : DbContext
    {

        public Xkom_ProjektContext(DbContextOptions<Xkom_ProjektContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cena> Cenas { get; set; }
        public virtual DbSet<Ilosc> Iloscs { get; set; }
        public virtual DbSet<Kategorie> Kategories { get; set; }
        public virtual DbSet<KategorieAll> KategorieAlls { get; set; }
        public virtual DbSet<Klient> Klients { get; set; }
        public virtual DbSet<KlientKonto> KlientKontos { get; set; }
        public virtual DbSet<Podkategorie> Podkategories { get; set; }
        public virtual DbSet<Produkt> Produkts { get; set; }
        public virtual DbSet<ProduktAll> ProduktAlls { get; set; }
        public virtual DbSet<ProduktOpi> ProduktOpis { get; set; }
        public virtual DbSet<StatusKoszykow> StatusKoszykows { get; set; }
        public virtual DbSet<StatusPlatnosci> StatusPlatnoscis { get; set; }
        public virtual DbSet<StatusZamowienium> StatusZamowienia { get; set; }
        public virtual DbSet<WartoscZamowienium> WartoscZamowienia { get; set; }
        public virtual DbSet<ZamowienieAll> ZamowienieAlls { get; set; }
        public virtual DbSet<Zamowienium> Zamowienia { get; set; }
        public virtual DbSet<ZdjProduktu> ZdjProduktus { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Cena>(entity =>
            {
                entity.ToTable("cena");

                entity.HasIndex(e => e.ProduktId, "cena_produkt_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenaBrutto).HasColumnName("cena_brutto");

                entity.Property(e => e.CenaNetto)
                    .HasColumnName("cena_netto")
                    .HasComputedColumnSql("([cena_brutto]/((1)+[stawka_vat]))", false);

                entity.Property(e => e.ProduktId).HasColumnName("produkt_id");

                entity.Property(e => e.StawkaVat).HasColumnName("stawka_vat");

                entity.Property(e => e.WartoscNetto)
                    .HasColumnName("Wartosc_netto")
                    .HasComputedColumnSql("([cena_brutto]-[cena_brutto]/((1)+[stawka_vat]))", false);

                entity.HasOne(d => d.Produkt)
                    .WithOne(p => p.Cena)
                    .HasForeignKey<Cena>(d => d.ProduktId)
                    .HasConstraintName("FK__cena__produkt_id__2F10007B");
            });

            modelBuilder.Entity<Ilosc>(entity =>
            {
                entity.ToTable("ilosc");

                entity.HasIndex(e => e.ProduktId, "ilosc_pordukt_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ilosc1).HasColumnName("ilosc");

                entity.Property(e => e.ProduktId).HasColumnName("produkt_id");

                entity.HasOne(d => d.Produkt)
                    .WithOne(p => p.Ilosc)
                    .HasForeignKey<Ilosc>(d => d.ProduktId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ilosc__produkt_i__2C3393D0");
            });

            modelBuilder.Entity<Kategorie>(entity =>
            {
                entity.ToTable("kategorie");

                entity.HasIndex(e => e.NazwaKategorii, "kategorie_nazwa_kategorii");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NazwaKategorii)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_kategorii");
            });

            modelBuilder.Entity<KategorieAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("kategorie_all");

                entity.Property(e => e.Kategoria)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Podkategoria)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Klient>(entity =>
            {
                entity.ToTable("klient");

                entity.HasIndex(e => e.Mail, "klient_mail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Imie)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("imie");

                entity.Property(e => e.KodPocztowy)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasColumnName("kod_pocztowy");

                entity.Property(e => e.Mail)
                    .HasMaxLength(320)
                    .IsUnicode(false)
                    .HasColumnName("mail");

                entity.Property(e => e.Miasto)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("miasto");

                entity.Property(e => e.NazwaFirmy)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_firmy");

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("nazwisko");

                entity.Property(e => e.Nip)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("nip");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("telefon");
            });

            modelBuilder.Entity<KlientKonto>(entity =>
            {
                entity.ToTable("klient_konto");

                entity.HasIndex(e => e.Mail, "klient_konto_mail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Haslo)
                    .IsRequired()
                    .HasMaxLength(256)
                    .HasColumnName("haslo")
                    .IsFixedLength(true);

                entity.Property(e => e.KlientId).HasColumnName("klient_id");

                entity.Property(e => e.Mail)
                    .IsRequired()
                    .HasMaxLength(32)
                    .HasColumnName("mail");

                entity.Property(e => e.Sol).HasColumnName("sol");

                entity.HasOne(d => d.Klient)
                    .WithMany(p => p.KlientKontos)
                    .HasForeignKey(d => d.KlientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__klient_ko__klien__412EB0B6");
            });

            modelBuilder.Entity<Podkategorie>(entity =>
            {
                entity.ToTable("podkategorie");

                entity.HasIndex(e => e.NazwaPodkategorii, "podkategorie_nazwa_podkategorii");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KategorieId).HasColumnName("kategorie_id");

                entity.Property(e => e.NazwaPodkategorii)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_podkategorii");

                entity.HasOne(d => d.Kategorie)
                    .WithMany(p => p.Podkategories)
                    .HasForeignKey(d => d.KategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__podkatego__kateg__25869641");
            });

            modelBuilder.Entity<Produkt>(entity =>
            {
                entity.ToTable("produkt");

                entity.HasIndex(e => e.KodProduktu, "produkt_kod_produktu");

                entity.HasIndex(e => e.NazwaProduktu, "produkt_nazwa_produktu");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.KategorieId).HasColumnName("kategorie_id");

                entity.Property(e => e.KodProduktu)
                    .HasColumnType("decimal(6, 0)")
                    .HasColumnName("Kod_produktu");

                entity.Property(e => e.NazwaProduktu)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Nazwa_produktu");

                entity.Property(e => e.PodkategorieId).HasColumnName("podkategorie_id");

                entity.HasOne(d => d.Kategorie)
                    .WithMany(p => p.Produkts)
                    .HasForeignKey(d => d.KategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__produkt__kategor__286302EC");

                entity.HasOne(d => d.Podkategorie)
                    .WithMany(p => p.Produkts)
                    .HasForeignKey(d => d.PodkategorieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__produkt__podkate__29572725");
            });

            modelBuilder.Entity<ProduktAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("produkt_all");

                entity.Property(e => e.CenaBrutto).HasColumnName("Cena Brutto");

                entity.Property(e => e.CenaNetto)
                    .HasMaxLength(4000)
                    .HasColumnName("Cena Netto");

                entity.Property(e => e.Kategoria)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.NazwaProduktu)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Nazwa Produktu");

                entity.Property(e => e.OpisProduktu)
                    .HasColumnType("text")
                    .HasColumnName("Opis Produktu");

                entity.Property(e => e.Podkategoria)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.StawkaVat).HasColumnName("Stawka Vat");
            });

            modelBuilder.Entity<ProduktOpi>(entity =>
            {
                entity.ToTable("produkt_opis");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Opis)
                    .HasColumnType("text")
                    .HasColumnName("opis");

                entity.Property(e => e.ProduktId).HasColumnName("produkt_id");

                entity.HasOne(d => d.Produkt)
                    .WithOne(p => p.ProduktOpi)
                    .HasForeignKey<ProduktOpi>(d => d.ProduktId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__produkt_o__produ__440B1D61");
            });

            modelBuilder.Entity<StatusKoszykow>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("status_koszykow");

                entity.Property(e => e.CenaBrutto).HasColumnName("cena_brutto");

                entity.Property(e => e.Ilosc).HasColumnName("ilosc");

                entity.Property(e => e.Mail)
                    .HasMaxLength(320)
                    .IsUnicode(false)
                    .HasColumnName("mail");

                entity.Property(e => e.NazwaProduktu)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_produktu");

                entity.Property(e => e.NazwaStatusuZamowienia)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_statusu_zamowienia");

                entity.Property(e => e.StatusPlatnosci)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("status_platnosci");

                entity.Property(e => e.Telefon)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("telefon");

                entity.Property(e => e.Wartosc).HasColumnName("wartosc");

                entity.Property(e => e.ZamowienieId).HasColumnName("zamowienie_id");
            });

            modelBuilder.Entity<StatusPlatnosci>(entity =>
            {
                entity.ToTable("status_platnosci");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.StatusPlatnosci1)
                    .HasMaxLength(21)
                    .IsUnicode(false)
                    .HasColumnName("status_platnosci");
            });

            modelBuilder.Entity<StatusZamowienium>(entity =>
            {
                entity.ToTable("status_zamowienia");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.NazwaStatusuZamowienia)
                    .IsRequired()
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("nazwa_statusu_zamowienia");
            });

            modelBuilder.Entity<WartoscZamowienium>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("wartosc_zamowienia");

                entity.Property(e => e.Imie)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("imie");

                entity.Property(e => e.KosztCałkowityBrutto)
                    .HasMaxLength(4000)
                    .HasColumnName("Koszt Całkowity Brutto");

                entity.Property(e => e.KosztCałkowityNetto)
                    .HasMaxLength(4000)
                    .HasColumnName("Koszt Całkowity Netto");

                entity.Property(e => e.KosztCałkowityStawkiVat)
                    .HasMaxLength(4000)
                    .HasColumnName("Koszt Całkowity Stawki VAT");

                entity.Property(e => e.Mail)
                    .HasMaxLength(320)
                    .IsUnicode(false)
                    .HasColumnName("mail");

                entity.Property(e => e.Nazwisko)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("nazwisko");

                entity.Property(e => e.NumerZamówienia).HasColumnName("Numer Zamówienia");
            });

            modelBuilder.Entity<ZamowienieAll>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("zamowienie_all");

                entity.Property(e => e.CenaCałkowitaBrutto).HasColumnName("Cena Całkowita Brutto");

                entity.Property(e => e.CenaCałkowitaNetto).HasColumnName("Cena Całkowita Netto");

                entity.Property(e => e.CenaJednostkowaBrutto).HasColumnName("Cena Jednostkowa Brutto");

                entity.Property(e => e.NazwaProduktu)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("Nazwa Produktu");

                entity.Property(e => e.NumerZamówienia).HasColumnName("Numer Zamówienia");
            });

            modelBuilder.Entity<Zamowienium>(entity =>
            {
                entity.ToTable("zamowienia");

                entity.HasIndex(e => e.CenaId, "zamowienia_cena_id");

                entity.HasIndex(e => e.IdKlienta, "zamowienia_klient_id");

                entity.HasIndex(e => e.ProduktId, "zamowienia_produkt_id");

                entity.HasIndex(e => e.Id, "zamowienia_zamowienie_id");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CenaId).HasColumnName("cena_id");

                entity.Property(e => e.DataZamowienia)
                    .HasColumnType("date")
                    .HasColumnName("data_zamowienia");

                entity.Property(e => e.IdKlienta).HasColumnName("id_klienta");

                entity.Property(e => e.Ilosc).HasColumnName("ilosc");

                entity.Property(e => e.ProduktId).HasColumnName("produkt_id");

                entity.Property(e => e.StatusPlatnosciId).HasColumnName("status_platnosci_id");

                entity.Property(e => e.StatusZamowieniaId).HasColumnName("status_zamowienia_id");

                entity.Property(e => e.Id).HasColumnName("zamowienie_id");

                entity.HasOne(d => d.Cena)
                    .WithMany(p => p.Zamowienia)
                    .HasForeignKey(d => d.CenaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__zamowieni__cena___38996AB5");

                entity.HasOne(d => d.IdKlientaNavigation)
                    .WithMany(p => p.Zamowienia)
                    .HasForeignKey(d => d.IdKlienta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__zamowieni__id_kl__398D8EEE");

                //entity.HasOne(d => d.Produkt)
                //    .WithMany(p => p.Zamowienia)
                //    .HasForeignKey(d => d.ProduktId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK__zamowieni__produ__37A5467C");

                entity.HasOne(d => d.StatusPlatnosci)
                    .WithMany(p => p.Zamowienia)
                    .HasForeignKey(d => d.StatusPlatnosciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__zamowieni__statu__3A81B327");

                entity.HasOne(d => d.StatusZamowienia)
                    .WithMany(p => p.Zamowienia)
                    .HasForeignKey(d => d.StatusZamowieniaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__zamowieni__statu__3B75D760");
            });

            modelBuilder.Entity<ZdjProduktu>(entity =>
            {
                entity.ToTable("zdj_produktu");

                entity.HasIndex(e => e.PathDoZdj, "zdj_produktu_path");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PathDoZdj)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("path_do_zdj");

                entity.Property(e => e.ProduktId).HasColumnName("produkt_id");

                entity.HasOne(d => d.Produkt)
                    .WithOne(p => p.ZdjProduktu)
                    .HasForeignKey<ZdjProduktu>(d => d.ProduktId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__zdj_produ__produ__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
