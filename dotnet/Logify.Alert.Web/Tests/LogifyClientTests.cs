﻿#if DEBUG
using System;
using DevExpress.Logify.Web;
using NUnit.Framework;
using DevExpress.Logify.Core.Internal;

namespace DevExpress.Logify.Core.Tests {
    [TestFixture]
    public class LogifyClientTests {
        LogifyAlert client;

        [SetUp]
        public void Setup() {
            this.client = new LogifyAlert(true);
        }
        [TearDown]
        public void TearDown() {
            this.client = null;
        }

        [Test]
        public void Defaults() {
            Assert.AreEqual(null, client.ApiKey);
            Assert.AreEqual(null, client.AppName);
            Assert.AreEqual(null, client.AppVersion);
            Assert.AreEqual(false, client.ConfirmSendReport);
            //Assert.AreEqual(null, client.MiniDumpServiceUrl);
            Assert.AreEqual("https://logify.devexpress.com/api/report/", client.ServiceUrl);
            Assert.AreEqual(null, client.UserId);
            Assert.AreEqual(null, client.ProxyCredentials);
            Assert.AreEqual(true, client.CustomData != null);
            Assert.AreEqual(0, client.CustomData.Count);
            Assert.AreEqual(true, client.Attachments != null);
            Assert.AreEqual(0, client.Attachments.Count);
            Assert.AreEqual("offline_reports", client.OfflineReportsDirectory);
            Assert.AreEqual(100, client.OfflineReportsCount);
            Assert.AreEqual(false, client.OfflineReportsEnabled);
            Assert.AreEqual(false, client.CollectBreadcrumbs);
            Assert.AreEqual(true, client.Breadcrumbs != null);
            Assert.AreEqual(0, client.Breadcrumbs.Count);
            //Assert.AreEqual(1000, client.BreadcrumbsMaxCount);
            Assert.AreEqual(null, client.IgnoreFormFields);
            Assert.AreEqual(null, client.IgnoreHeaders);
            Assert.AreEqual(null, client.IgnoreCookies);
            Assert.AreEqual(null, client.IgnoreServerVariables);

            Predicate<IExceptionReportSender> predicate = (s) => {
                OfflineDirectoryExceptionReportSender sender = s as OfflineDirectoryExceptionReportSender;
                if (sender != null) {
                    Assert.AreEqual("offline_reports", sender.DirectoryName);
                    Assert.AreEqual(100, sender.ReportCount);
                    Assert.AreEqual(false, sender.IsEnabled);
                }
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }

        [Test]
        public void ApiKey() {
            Assert.AreEqual(null, client.ApiKey);
            client.ApiKey = "<my-api-key>";
            Assert.AreEqual("<my-api-key>", client.ApiKey);
            Predicate<IExceptionReportSender> predicate = (s) => {
                Assert.AreEqual("<my-api-key>", s.ApiKey);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        [Test]
        public void ServiceUrl() {
            Assert.AreEqual("https://logify.devexpress.com/api/report/", client.ServiceUrl);
            client.ServiceUrl = "<my-service>";
            Assert.AreEqual("<my-service>", client.ServiceUrl);
            Predicate<IExceptionReportSender> predicate = (s) => {
                Assert.AreEqual("<my-service>", s.ServiceUrl);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        /*
        [Test]
        public void MiniDumpServiceUrl() {
            Assert.AreEqual(null, client.MiniDumpServiceUrl);
            client.MiniDumpServiceUrl = "<my-minidump-service>";
            Assert.AreEqual("<my-minidump-service>", client.MiniDumpServiceUrl);
            Predicate<IExceptionReportSender> predicate = (s) => {
                Assert.AreEqual("<my-minidump-service>", s.MiniDumpServiceUrl);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        */
        [Test]
        public void ConfirmSendReport() {
            Assert.AreEqual(false, client.ConfirmSendReport);

            client.ConfirmSendReport = true;
            Assert.AreEqual(true, client.ConfirmSendReport);
            Predicate<IExceptionReportSender> predicate = (s) => {
                Assert.AreEqual(true, s.ConfirmSendReport);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);

            client.ConfirmSendReport = false;
            Assert.AreEqual(false, client.ConfirmSendReport);
            predicate = (s) => {
                Assert.AreEqual(false, s.ConfirmSendReport);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        [Test]
        public void ProxyCredentials() {
            Assert.AreEqual(null, client.ProxyCredentials);

            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("test-username", "test-password");
            client.ProxyCredentials = credentials;
            Assert.AreNotEqual(null, client.ProxyCredentials);
            Predicate<IExceptionReportSender> predicate = (s) => {
                Assert.AreNotEqual(null, s.ProxyCredentials);
                Assert.AreEqual(credentials, s.ProxyCredentials);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);

            client.ProxyCredentials = null;
            Assert.AreEqual(null, client.ProxyCredentials);
            predicate = (s) => {
                Assert.AreEqual(null, s.ProxyCredentials);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        [Test]
        public void OfflineReportsDirectory() {
            Assert.AreEqual("offline_reports", client.OfflineReportsDirectory);

            client.OfflineReportsDirectory = "offline_reports2";
            Assert.AreEqual("offline_reports2", client.OfflineReportsDirectory);
            Predicate<IExceptionReportSender> predicate = (s) => {
                OfflineDirectoryExceptionReportSender sender = s as OfflineDirectoryExceptionReportSender;
                if (sender != null)
                    Assert.AreEqual("offline_reports2", sender.DirectoryName);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        [Test]
        public void OfflineReportsCount() {
            Assert.AreEqual(100, client.OfflineReportsCount);

            client.OfflineReportsCount = 20;
            Assert.AreEqual(20, client.OfflineReportsCount);
            Predicate<IExceptionReportSender> predicate = (s) => {
                OfflineDirectoryExceptionReportSender sender = s as OfflineDirectoryExceptionReportSender;
                if (sender != null)
                    Assert.AreEqual(20, sender.ReportCount);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        [Test]
        public void OfflineReportsEnabled() {
            Assert.AreEqual(false, client.OfflineReportsEnabled);

            client.OfflineReportsEnabled = true;
            Assert.AreEqual(true, client.OfflineReportsEnabled);
            Predicate<IExceptionReportSender> predicate = (s) => {
                OfflineDirectoryExceptionReportSender sender = s as OfflineDirectoryExceptionReportSender;
                if (sender != null)
                    Assert.AreEqual(true, sender.IsEnabled);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);

            client.OfflineReportsEnabled = false;
            Assert.AreEqual(false, client.OfflineReportsEnabled);
            predicate = (s) => {
                OfflineDirectoryExceptionReportSender sender = s as OfflineDirectoryExceptionReportSender;
                if (sender != null)
                    Assert.AreEqual(false, sender.IsEnabled);
                return true;
            };
            CheckDefaultStructureAndPredicate(client, predicate);
        }
        [Test]
        public void CollectBreadcrumbs() {
            Assert.AreEqual(false, client.CollectBreadcrumbs);

            client.CollectBreadcrumbs = true;
            Assert.AreEqual(true, client.CollectBreadcrumbs);

            client.CollectBreadcrumbs = false;
            Assert.AreEqual(false, client.CollectBreadcrumbs);
        }
        /*
        [Test]
        public void BreadcrumbsMaxCount() {
            Assert.AreEqual(1000, client.BreadcrumbsMaxCount);

            client.BreadcrumbsMaxCount = 100;
            Assert.AreEqual(100, client.BreadcrumbsMaxCount);

            client.BreadcrumbsMaxCount = 200;
            Assert.AreEqual(200, client.BreadcrumbsMaxCount);
        }
        */
        [Test]
        public void IgnoreHeaders() {
            Assert.AreEqual(null, client.IgnoreHeaders);

            client.IgnoreHeaders = "*";
            Assert.AreEqual("*", client.IgnoreHeaders);

            client.IgnoreHeaders = "name,name1, name2";
            Assert.AreEqual("name,name1, name2", client.IgnoreHeaders);
        }
        [Test]
        public void IgnoreCookies() {
            Assert.AreEqual(null, client.IgnoreCookies);

            client.IgnoreCookies = "*";
            Assert.AreEqual("*", client.IgnoreCookies);

            client.IgnoreCookies = "name,name1, name2";
            Assert.AreEqual("name,name1, name2", client.IgnoreCookies);
        }
        [Test]
        public void IgnoreServerVariables() {
            Assert.AreEqual(null, client.IgnoreServerVariables);

            client.IgnoreServerVariables = "*";
            Assert.AreEqual("*", client.IgnoreServerVariables);

            client.IgnoreServerVariables = "name,name1, name2";
            Assert.AreEqual("name,name1, name2", client.IgnoreServerVariables);
        }
        [Test]
        public void IgnoreFormFields() {
            Assert.AreEqual(null, client.IgnoreFormFields);

            client.IgnoreFormFields = "*";
            Assert.AreEqual("*", client.IgnoreFormFields);

            client.IgnoreFormFields = "name,name1, name2";
            Assert.AreEqual("name,name1, name2", client.IgnoreFormFields);
        }

        static void CheckDefaultStructureAndPredicate(LogifyAlert client, Predicate<IExceptionReportSender> predicate) {
            IExceptionReportSender sender = ExceptionLoggerFactory.Instance.PlatformReportSender;
            Assert.AreEqual(true, sender != null);
            Assert.AreEqual(typeof(EmptyBackgroundExceptionReportSender), sender.GetType());
            CheckSenderConsistency(client, sender);
            predicate(sender);

            sender = ((EmptyBackgroundExceptionReportSender)sender).InnerSender;
            Assert.AreEqual(true, sender != null);
            Assert.AreEqual(typeof(CompositeExceptionReportSender), sender.GetType());
            CheckSenderConsistency(client, sender);
            predicate(sender);

            CompositeExceptionReportSender compositeSender = (CompositeExceptionReportSender)sender;
            CheckSenderConsistency(client, compositeSender);
            predicate(compositeSender);
            Assert.AreEqual(true, compositeSender.Senders != null);
            Assert.AreEqual(2, compositeSender.Senders.Count);

            sender = compositeSender.Senders[0];
            Assert.AreEqual(true, sender != null);
            Assert.AreEqual(typeof(WebExceptionReportSender), sender.GetType());
            CheckSenderConsistency(client, sender);
            predicate(sender);

            sender = compositeSender.Senders[1];
            Assert.AreEqual(true, sender != null);
            Assert.AreEqual(typeof(OfflineDirectoryExceptionReportSender), sender.GetType());
            CheckSenderConsistency(client, sender);
            predicate(sender);
        }

        static void CheckSenderConsistency(LogifyClientBase client, IExceptionReportSender sender) {
            //Assert.AreEqual(client.MiniDumpServiceUrl, sender.MiniDumpServiceUrl);
            Assert.AreEqual(client.ServiceUrl, sender.ServiceUrl);
            Assert.AreEqual(client.ConfirmSendReport, sender.ConfirmSendReport);
            Assert.AreEqual(client.ApiKey, sender.ApiKey);
        }
    }
}
#endif