using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using tarea2.Model;
using SQLite;

namespace tarea2.Controller
{
    public class Database
    {
        readonly SQLiteAsyncConnection db;

        public Database(String pathdb)
        {
            db = new SQLiteAsyncConnection(pathdb);

            db.CreateTableAsync<Signatures>().Wait();
        }

        public Task<List<Signatures>> GetListSignatures()
        {
            return db.Table<Signatures>().ToListAsync();
        }

        public Task<Signatures> GetSignatureByCode(int signatureCode)
        {
            return db.Table<Signatures>()
                .Where(i => i.code == signatureCode)
                .FirstOrDefaultAsync();
        }

        public Task<int> saveSignature(Signatures signatures)
        {
            return signatures.code != 0 ? db.UpdateAsync(signatures) : db.InsertAsync(signatures);
        }

        public Task<int> deleteSignature(Signatures signatures)
        {
            return db.DeleteAsync(signatures);
        }
    }
}