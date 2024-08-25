using EShop.Domain.Domain;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepo;

        public PlayerService(IRepository<Player> playerRepo)
        {
            _playerRepo = playerRepo;
        }

        public void CreateNewPlayer(Player p)
        {
            _playerRepo.Insert(p);
        }

        public void DeletePlayer(Guid id)
        {
            Player ms = _playerRepo.Get(id);
            _playerRepo.Delete(ms);
        }

        public List<Player> GetAllPlayers()
        {
            return _playerRepo.GetAll().ToList();
        }

        public Player GetDetailsForPlayer(Guid? id)
        {
            return _playerRepo.Get(id);
        }

        public void UpdateExistingPlayer(Player p)
        {
            _playerRepo.Update(p);
        }
    }
}
