using MedHealth.Catalog.Dal.Interfaces;

namespace MedHealth.Catalog.Dal.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _context;
    private IDoctorRepository? _doctorRepository;

    public UnitOfWork(CatalogDbContext context)
    {
        _context = context;
    }

    public IDoctorRepository Doctors => 
        _doctorRepository ??= new DoctorRepository(_context);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}