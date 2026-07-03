using Gym.Data.contexts;
using Gym.DataAccess.Repositories;
using Gym.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext _context;
        public UnitOfWork(GymDbcontext dbcontext) { 
        _context = dbcontext;
        }

        private IMemberRepository _members;
        private IPlanRepository _plans;
        private ITrainerRepository _trainers;
        private IRepository<HealthRecord> _healthRecords;
        private IRepository<Person> _persons;
        private IRepository<Category> _categories;
        private ISessionRepository _session;
        private IMembershipRepository _membership;
        private IBookingRepository _bookings;


        public IMemberRepository Members => _members??= new MemberRepository(_context);

        public IPlanRepository Plans => _plans??= new PlanRepository(_context);

        public ITrainerRepository Trainers => _trainers??= new TrainerRepository(_context);

        public IRepository<HealthRecord> HealthRecords => _healthRecords??= new Repository<HealthRecord>(_context);

        public IRepository<Person> Persons => _persons??= new Repository<Person>(_context);
        public IRepository<Category> Categories => _categories ??= new Repository<Category>(_context);

        public ISessionRepository Sessions => _session ??= new SessionRepository(_context);
        public IMembershipRepository Memberships => _membership ??= new MembershipRepository(_context);
        public IBookingRepository Bookings => _bookings ??= new BookingRepository(_context);


        public Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

     }
 }
