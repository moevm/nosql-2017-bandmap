﻿using System.Linq;
using Funkmap.Auth.Data;
using Funkmap.Musician.Data;
using Funkmap.Tests.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Funkmap.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void SeedData()
        {
            var musicianContext = new FakeMusicianDbContext();
            var musicianRepository = new MusicianRepository(musicianContext);
            var musicians = musicianRepository.GetAllAsync().Result.ToList();

            var authContext = new FakeAuthDbContext();
            var authRepository = new AuthRepository(authContext);
            var users = authRepository.GetAllAsync().Result.ToList();


        }
    }
}