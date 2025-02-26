﻿using System;
using System.Threading.Tasks;

using EFHooks.Tests.Hooks;
using NUnit.Framework;

namespace EFHooks.Tests
{
    partial class HookedDbContextTests
    {
        [Test]
        public async Task HookedDbContext_MustCallHooks_WhenRunningSaveChangesAsync()
        {
            var hooks = new IHook[]
                            {
                                new TimestampPreInsertHook()
                            };

            var context = new LocalContext(hooks);
            var entity = new TimestampedSoftDeletedEntity();
            context.Entities.Add(entity);
            await context.SaveChangesAsync();

            Assert.AreEqual(entity.CreatedAt.Date, DateTime.Today);
        }
    }
}
