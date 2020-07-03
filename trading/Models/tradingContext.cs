using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace trading.Models
{
    public partial class tradingContext : DbContext
    {
        public tradingContext()
        {
        }

        public tradingContext(DbContextOptions<tradingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ReportCommission1> ReportCommission1 { get; set; }
        public virtual DbSet<ReportCommission2> ReportCommission2 { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<AccountAdjust> AccountAdjust { get; set; }
        public virtual DbSet<AccountAdjustView> AccountAdjustView { get; set; }
        public virtual DbSet<AccountTransfer> AccountTransfer { get; set; }
        public virtual DbSet<AccountTransferView> AccountTransferView { get; set; }
        public virtual DbSet<AccountTxn> AccountTxn { get; set; }
        public virtual DbSet<AccountTxnView> AccountTxnView { get; set; }
        public virtual DbSet<ReportAccountBalance> ReportAccountBalance { get; set; }
        public virtual DbSet<ReportAccountPayable> ReportAccountPayable { get; set; }
        public virtual DbSet<ReportAccountReceivable> ReportAccountReceivable { get; set; } 
        public virtual DbSet<CurrencyPairView> CurrencyPairView { get; set; }
        public virtual DbSet<AccountBankAccount> AccountBankAccount { get; set; }
        public virtual DbSet<AccountBankAccountView> AccountBankAccountView { get; set; }
        public virtual DbSet<ProviderBankAccount> ProviderBankAccount { get; set; }
        public virtual DbSet<ProviderBankAccountView> ProviderBankAccountView { get; set; }
        public virtual DbSet<ProviderTradingProfile> ProviderTradingProfile { get; set; }
        public virtual DbSet<ProviderTradingProfileIntroducerMap> ProviderTradingProfileIntroducerMap { get; set; }
        public virtual DbSet<ProviderTradingProfileIntroducerMapView> ProviderTradingProfileIntroducerMapView { get; set; }
        public virtual DbSet<ProviderTradingProfileView> ProviderTradingProfileView { get; set; }
        public virtual DbSet<DfrView> DfrView { get; set; }
        public virtual DbSet<SenderView> SenderView { get; set; }
        public virtual DbSet<CostRateView> CostRateView { get; set; }
        public virtual DbSet<ReportTxn> ReportTxn { get; set; }
        public virtual DbSet<TxnCompleteView> TxnCompleteView { get; set; }
        public virtual DbSet<ReportAccount> ReportAccount { get; set; }
        public virtual DbSet<ReportProvider> ReportProvider { get; set; }
        public virtual DbSet<PayoutView> PayoutView { get; set; }
        public virtual DbSet<SlipView> SlipView { get; set; }
        public virtual DbSet<Payout> Payout { get; set; }
        public virtual DbSet<Slip> Slip { get; set; }
        public virtual DbSet<TxnView> TxnView { get; set; }
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<CostRate> CostRate { get; set; }
        public virtual DbSet<CurrencyPair> CurrencyPair { get; set; }
        public virtual DbSet<Dfr> Dfr { get; set; }
        public virtual DbSet<Provider> Provider { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Txn> Txn { get; set; }
        public virtual DbSet<Sender> Sender { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }
        public virtual DbSet<ClientBankAccount> ClientBankAccount { get; set; }
        public virtual DbSet<ClientBankAccountView> ClientBankAccountView { get; set; }
        public virtual DbSet<ClientTradingProfile> ClientTradingProfile { get; set; }
        public virtual DbSet<ClientTradingProfileIntroducerMap> ClientTradingProfileIntroducerMap { get; set; }
        public virtual DbSet<ClientTradingProfileIntroducerMapView> ClientTradingProfileIntroducerMapView { get; set; }
        public virtual DbSet<ClientTradingProfileView> ClientTradingProfileView { get; set; }
        public virtual DbSet<ClientType> ClientType { get; set; }
        public virtual DbSet<ClientView> ClientView { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Introducer> Introducer { get; set; }
        public virtual DbSet<IntroducerBankAccount> IntroducerBankAccount { get; set; }
        public virtual DbSet<IntroducerBankAccountView> IntroducerBankAccountView { get; set; }
        public virtual DbSet<IntroducerCommissionType> IntroducerCommissionType { get; set; }
        public virtual DbSet<IntroducerView> IntroducerView { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SQL5053.site4now.net;Initial Catalog=DB_A6159D_tradingDEV;User Id=DB_A6159D_tradingDEV_admin;Password=phoenix1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportCommission1>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReportCommission1");

                entity.Property(e => e.ClientAmountIn).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientAmountOut).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientCurrencyNameIn)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientCurrencyNameOut)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerCommissionRate).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.IntroducerCommissionUSD)
                    .HasColumnName("IntroducerCommissionUSD")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");
            });

            modelBuilder.Entity<ReportCommission2>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReportCommission2");

                entity.Property(e => e.ClientAmountIn).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientAmountOut).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientCurrencyNameIn)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientCurrencyNameOut)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerCommissionRate).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.IntroducerCommissionUSD)
                    .HasColumnName("IntroducerCommissionUSD")
                    .HasColumnType("decimal(38, 9)");

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<AccountAdjust>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Reference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountAdjustView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccountAdjustView");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Reference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountTransfer>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountBankAccountIDFrom).HasColumnName("AccountBankAccountIDFrom");

                entity.Property(e => e.AccountBankAccountIDTo).HasColumnName("AccountBankAccountIDTo");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Reference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountTransferView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccountTransferView");

                entity.Property(e => e.AccountBankAccountIDFrom).HasColumnName("AccountBankAccountIDFrom");

                entity.Property(e => e.AccountBankAccountIDTo).HasColumnName("AccountBankAccountIDTo");

                entity.Property(e => e.AccountBankAccountNameFrom)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountBankAccountNameTo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Reference)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountTxn>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.AmountCredit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.AmountDebit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.ReferenceID).HasColumnName("ReferenceID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<AccountTxnView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccountTxnView");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AmountCredit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.AmountDebit).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ReferenceID).HasColumnName("ReferenceID");

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ReportAccountBalance>(entity =>
            {
                //entity.HasKey(e => e.id);
                entity.HasNoKey();

                entity.ToView("ReportAccountBalance");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Balance).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.BalanceUSD)
                    .HasColumnName("BalanceUSD")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<ReportAccountPayable>(entity =>
            {
                entity.HasNoKey();

                //entity.HasKey(e => e.ClientTradingProfileID);

                entity.ToView("ReportAccountPayable");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientAmountUncompleted).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.ClientAmountUncompletedUSD)
                    .HasColumnName("ClientAmountUncompletedUSD")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.ClientCurrencyNameOut)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TradeDate).HasColumnType("date");
            });

            modelBuilder.Entity<ReportAccountReceivable>(entity =>
            {
                entity.HasNoKey();

                //entity.HasKey(e => e.ProviderTradingProfileID); 
                 
                entity.ToView("ReportAccountReceivable");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.AmountReceivable).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.AmountReceivableUSD)
                    .HasColumnName("AmountReceivableUSD")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.CurrencyName2)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");
            });
            modelBuilder.Entity<ReportTxn>(entity =>
            {
                entity.HasNoKey();
                //entity.HasKey("TxnID");
                entity.ToView("ReportTxn");

                entity.Property(e => e.ClientAmountOut).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientCurrencyNameOut)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientPayoutUSD)
                    .HasColumnName("ClientPayoutUSD")
                    .HasColumnType("decimal(38, 13)");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");
                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");
                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.GrossProfitUSD)
                    .HasColumnName("GrossProfitUSD")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.GrossProfitUSDPct)
                    .HasColumnName("GrossProfitUSDPct")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.IntroducerCommissionUSD1)
                    .HasColumnName("IntroducerCommissionUSD1")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.IntroducerCommissionUSD2)
                    .HasColumnName("IntroducerCommissionUSD2")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.ProviderPayinUSD)
                    .HasColumnName("ProviderPayinUSD")
                    .HasColumnType("decimal(33, 15)");

                entity.Property(e => e.ClientPayoutOrig)
                    .HasColumnName("ClientPayoutOrig")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.ClientPayoutMissing)
                    .HasColumnName("ClientPayoutMissing")
                    .HasColumnType("decimal(38, 6)");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");
            });
            modelBuilder.Entity<CurrencyPairView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CurrencyPairView");

                entity.Property(e => e.CurrencyName1)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CurrencyName2)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CurrencyPairName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.CurrencyPairName2)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.CurrencyPairID2).HasColumnName("CurrencyPairID2");
                entity.Property(e => e.id1).HasColumnName("id1");

                entity.Property(e => e.id2).HasColumnName("id2");
            });
            modelBuilder.Entity<AccountBankAccount>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountHolderAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountID).HasColumnName("AccountID");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountBankAccountView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("AccountBankAccountView");

                entity.Property(e => e.AccountHolderAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountID).HasColumnName("AccountID");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.AccountTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.MasterAccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProviderBankAccount>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountHolderAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderID).HasColumnName("ProviderID");

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProviderBankAccountView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProviderBankAccountView");

                entity.Property(e => e.AccountHolderAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.AccountTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ProviderID).HasColumnName("ProviderID");

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProviderTradingProfile>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.CurrencyPairID).HasColumnName("CurrencyPairID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.ProviderID).HasColumnName("ProviderID");

                entity.Property(e => e.ProviderTradingProfileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProviderTradingProfileIntroducerMap>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IntroducerCommissionRate).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.IntroducerCommissionTypeID).HasColumnName("IntroducerCommissionTypeID");

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");
            });

            modelBuilder.Entity<ProviderTradingProfileIntroducerMapView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProviderTradingProfileIntroducerMapView");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.IntroducerCommissionRate).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.IntroducerCommissionTypeID).HasColumnName("IntroducerCommissionTypeID");

                entity.Property(e => e.IntroducerCommissionTypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.IntroducerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProviderTradingProfileView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ProviderTradingProfileView");

                entity.Property(e => e.CurrencyPairID).HasColumnName("CurrencyPairID");

                entity.Property(e => e.CurrencyPairName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ProviderID).HasColumnName("ProviderID");

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderTradingProfileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");
            });
            modelBuilder.Entity<DfrView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DfrView");

                entity.Property(e => e.CurrencyPairID).HasColumnName("CurrencyPairID");

                entity.Property(e => e.CurrencyPairName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TradeDate).HasColumnType("date");
            });


            modelBuilder.Entity<SenderView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SenderView");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.DirectorFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DirectorLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderID).HasColumnName("ProviderID");

                entity.Property(e => e.ProviderName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SenderName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TxnCompleteView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TxnCompleteView");

                entity.Property(e => e.ClientAmountIn).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientAmountOut).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientCurrencyIDIn).HasColumnName("ClientCurrencyIDIn");

                entity.Property(e => e.ClientCurrencyIDOut).HasColumnName("ClientCurrencyIDOut");

                entity.Property(e => e.ClientCurrencyNameIn)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientCurrencyNameOut)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientCurrencyPairID).HasColumnName("ClientCurrencyPairID");

                entity.Property(e => e.ClientCurrencyPairName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ClientDfrRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ClientExRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientUniqueDfr).HasColumnName("ClientUniqueDfr");

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientPayOutAccountID).HasColumnName("ClientPayOutAccountID");

                entity.Property(e => e.ClientPayOutAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientPriceRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.LinkedDepositID).HasColumnName("LinkedDepositID");

                entity.Property(e => e.LinkedDepositReferenceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LinkedProviderCostDate).HasColumnType("date");

                entity.Property(e => e.LinkedProviderCostRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.LinkedProviderCurrencyID).HasColumnName("LinkedProviderCurrencyID");

                entity.Property(e => e.LinkedProviderCurrencyName)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LinkedProviderExpectedPayInAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.LinkedProviderTradingProfileID).HasColumnName("LinkedProviderTradingProfileID");

                entity.Property(e => e.LinkedProviderTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LinkedProviderBankAccountID).HasColumnName("LinkedProviderBankAccountID");

                entity.Property(e => e.LinkedProviderBankAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderCostDate).HasColumnType("date");

                entity.Property(e => e.ProviderCostRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ProviderCurrencyID).HasColumnName("ProviderCurrencyID");

                entity.Property(e => e.ProviderCurrencyName)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProviderExpectedPayInAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderBankAccountID).HasColumnName("ProviderBankAccountID");

                entity.Property(e => e.ProviderBankAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });
            modelBuilder.Entity<ReportAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReportAccount");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.TotalClientAmountOut).HasColumnType("decimal(38, 4)");
            });

            modelBuilder.Entity<ReportProvider>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ReportProvider");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.MissingSlipAmount).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.ProviderTradingProfileName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalProviderExpectedPayInAmount).HasColumnType("decimal(38, 4)");

                entity.Property(e => e.TotalSlipAmount).HasColumnType("decimal(38, 4)");
                entity.Property(e => e.TotalActualAmount).HasColumnType("decimal(38, 4)");
            });
            modelBuilder.Entity<PayoutView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("PayoutView");

                entity.Property(e => e.ClientPayoutAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientPayoutCurrencyID).HasColumnName("ClientPayoutCurrencyID");

                entity.Property(e => e.ClientPayoutCurrencyName)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientPayoutUSDRate)
                    .HasColumnName("ClientPayoutUSDRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ProviderPayinAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProviderPayinCurrencyID).HasColumnName("ProviderPayinCurrencyID");

                entity.Property(e => e.ProviderPayinCurrencyName)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProviderPayinUSDRate)
                    .HasColumnName("ProviderPayinUSDRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");
                
                entity.Property(e => e.TxnReferenceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UsedAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UsedClientPayoutFXRate)
                    .HasColumnName("UsedClientPayoutFXRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.UsedCurrencyID).HasColumnName("UsedCurrencyID");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");
                entity.Property(e => e.LinkedProviderTradingProfileID).HasColumnName("LinkedProviderTradingProfileID");
                entity.Property(e => e.UsedCurrencyName)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LinkedProviderTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AccountName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UsedUSDRate)
                    .HasColumnName("UsedUSDRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");
            });

            modelBuilder.Entity<SlipView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SlipView");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.AccountBankAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SenderID).HasColumnName("SenderID");

                entity.Property(e => e.SenderName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SlipAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");

                entity.Property(e => e.TxnReferenceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SlipDate).HasColumnType("date");
            });

            modelBuilder.Entity<Payout>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ClientPayoutAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientPayoutCurrencyID).HasColumnName("ClientPayoutCurrencyID");

                entity.Property(e => e.ClientPayoutUSDRate)
                    .HasColumnName("ClientPayoutUSDRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.ProviderPayinAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProviderPayinCurrencyID).HasColumnName("ProviderPayinCurrencyID");

                entity.Property(e => e.ProviderPayinUSDRate)
                    .HasColumnName("ProviderPayinUSDRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.UsedAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UsedClientPayoutFXRate)
                    .HasColumnName("UsedClientPayoutFXRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.UsedCurrencyID).HasColumnName("UsedCurrencyID");

                entity.Property(e => e.UsedUSDRate)
                    .HasColumnName("UsedUSDRate")
                    .HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Slip>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountBankAccountID).HasColumnName("AccountBankAccountID");

                entity.Property(e => e.ActualAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SenderID).HasColumnName("SenderID");

                entity.Property(e => e.SlipAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.TxnID).HasColumnName("TxnID");
                 
                entity.Property(e => e.SlipDate).HasColumnType("date");
            });

            modelBuilder.Entity<TxnView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("TxnView");

                entity.Property(e => e.ClientAmountIn).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientAmountOut).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientCurrencyIDIn).HasColumnName("ClientCurrencyIDIn");

                entity.Property(e => e.ClientCurrencyIDOut).HasColumnName("ClientCurrencyIDOut");

                entity.Property(e => e.ClientCurrencyNameIn)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientCurrencyNameOut)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ClientCurrencyPairID).HasColumnName("ClientCurrencyPairID");

                entity.Property(e => e.ClientCurrencyPairName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ClientDfrRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ClientExRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientUniqueDfr).HasColumnName("ClientUniqueDfr");

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientPayOutAccountID).HasColumnName("ClientPayOutAccountID");

                entity.Property(e => e.ClientPayOutAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ClientPriceRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.LinkedDepositID).HasColumnName("LinkedDepositID");

                entity.Property(e => e.LinkedDepositReferenceNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderCostDate).HasColumnType("date");

                entity.Property(e => e.ProviderCostRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ProviderCurrencyID).HasColumnName("ProviderCurrencyID");

                entity.Property(e => e.ProviderCurrencyName)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ProviderExpectedPayInAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderBankAccountID).HasColumnName("ProviderBankAccountID");

                entity.Property(e => e.ProviderBankAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Alert)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Sender>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.DirectorFirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DirectorLastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderID).HasColumnName("ProviderID");

                entity.Property(e => e.SenderName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Unit)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Txn>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ClientAmountIn).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientAmountOut).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ClientCurrencyIDIn).HasColumnName("ClientCurrencyIDIn");

                entity.Property(e => e.ClientCurrencyIDOut).HasColumnName("ClientCurrencyIDOut");

                entity.Property(e => e.ClientCurrencyPairID).HasColumnName("ClientCurrencyPairID");

                entity.Property(e => e.ClientDfrRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ClientExRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientUniqueDfr).HasColumnName("ClientUniqueDfr");

                entity.Property(e => e.ClientPayOutAccountID).HasColumnName("ClientPayOutAccountID");

                entity.Property(e => e.ClientPriceRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.LinkedDepositID).HasColumnName("LinkedDepositID");

                entity.Property(e => e.ProviderCostDate).HasColumnType("date");

                entity.Property(e => e.ProviderCostRate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ProviderCurrencyID).HasColumnName("ProviderCurrencyID");

                entity.Property(e => e.ProviderExpectedPayInAmount).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ProviderBankAccountID).HasColumnName("ProviderBankAccountID");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
            });




            modelBuilder.Entity<CostRate>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.ProviderTradingProfileID)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TradeDate).HasColumnType("date");
            });

            modelBuilder.Entity<CurrencyPair>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.CurrencyPairName)
                    .IsRequired()
                    .HasColumnName("CurrencyPairName")
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<Dfr>(entity =>
            {
                entity.ToTable("Dfr");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.CurrencyPairID)
                    .IsRequired()
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TradeDate).HasColumnType("date");
            });

            modelBuilder.Entity<Provider>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.ProviderName)
                    .IsRequired()
                    .HasColumnName("ProviderName")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<CostRateView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("CostRateView");

                entity.Property(e => e.CostFXRatio)
                    .HasColumnName("CostFXRatio")
                    .HasColumnType("decimal(26, 15)");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ProviderTradingProfileID).HasColumnName("ProviderTradingProfileID");

                entity.Property(e => e.ProviderTradingProfileName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rate).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.TradeDate).HasColumnType("date");
            });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AccountTypeName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });



            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ClientLegalName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientTypeID).HasColumnName("ClientTypeID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<ClientBankAccount>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountHolderAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientID).HasColumnName("ClientID");

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientBankAccountView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ClientBankAccountView");

                entity.Property(e => e.AccountHolderAddress)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.AccountTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientID).HasColumnName("ClientID");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientTradingProfile>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ClientID).HasColumnName("ClientID");

                entity.Property(e => e.ClientTradingProfileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyPairID).HasColumnName("CurrencyPairID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<ClientTradingProfileIntroducerMap>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IntroducerCommissionRate).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.IntroducerCommissionTypeID).HasColumnName("IntroducerCommissionTypeID");

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");
            });

            modelBuilder.Entity<ClientTradingProfileIntroducerMapView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ClientTradingProfileIntroducerMapView");

                entity.Property(e => e.ClientTradingProfileID).HasColumnName("ClientTradingProfileID");

                entity.Property(e => e.ClientTradingProfileName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.IntroducerCommissionRate).HasColumnType("decimal(7, 4)");

                entity.Property(e => e.IntroducerCommissionTypeID).HasColumnName("IntroducerCommissionTypeID");

                entity.Property(e => e.IntroducerCommissionTypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.IntroducerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientTradingProfileView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ClientTradingProfileView");

                entity.Property(e => e.ClientID).HasColumnName("ClientID");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientTradingProfileName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyPairID).HasColumnName("CurrencyPairID");

                entity.Property(e => e.CurrencyPairName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.IntroducerInfo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 4)");
            });

            modelBuilder.Entity<ClientType>(entity =>
            {
                entity.Property(e => e.ClientTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ClientView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ClientView");

                entity.Property(e => e.ClientLegalName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientTypeID).HasColumnName("ClientTypeID");

                entity.Property(e => e.ClientTypeName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.ClientTradingProfileInfo)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");
            });

            modelBuilder.Entity<Introducer>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IntroducerLegalName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IntroducerBankAccount>(entity =>
            {
                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.AccountHolderAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IntroducerBankAccountView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IntroducerBankAccountView");

                entity.Property(e => e.AccountHolderAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.AccountHolderName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AccountTypeID).HasColumnName("AccountTypeID");

                entity.Property(e => e.AccountTypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BranchAddress)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BSB)
                    .HasColumnName("BSB")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CountryID).HasColumnName("CountryID");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyID).HasColumnName("CurrencyID");

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.IBAN)
                    .HasColumnName("IBAN")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.id).HasColumnName("id");

                entity.Property(e => e.IntroducerID).HasColumnName("IntroducerID");

                entity.Property(e => e.IntroducerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Reference)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SWIFT)
                    .IsRequired()
                    .HasColumnName("SWIFT")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IntroducerCommissionType>(entity =>
            {
                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IntroducerCommissionTypeName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IntroducerView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IntroducerView");

                entity.Property(e => e.AssociatedClient)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateTimeAdded).HasColumnType("datetime");

                entity.Property(e => e.DateTimeModified).HasColumnType("datetime");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IntroducerLegalName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IntroducerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
