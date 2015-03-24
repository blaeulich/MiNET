using System;
using System.Collections.Generic;
using System.Linq;
using MiNET.Entities;

namespace MiNET.Worlds
{
	public class EntityManager
	{
		public const int EntityIdUndefined = -1;

		private static int _entityId = 1;

		private List<Entity> _entities = new List<Entity>();

		public EntityManager()
		{
			//_entities.Add(new Entity(-1, null));
		}

		public void AddEntity(Entity caller, Entity entity)
		{
			if (caller != null && entity != caller) throw new Exception("Tried to ADD entity for someone else. Should be the entity himself adding");

			lock (_entities)
			{
				if (entity.EntityId == EntityIdUndefined) entity.EntityId = _entityId++;

				if (!_entities.Contains(entity)) _entities.Add(entity);
			}
		}

		public void RemoveEntity(Entity caller, Entity entity)
		{
			if (entity == caller) throw new Exception("Tried to REMOVE entity for self");

			lock (_entities)
			{
				if (!_entities.Contains(entity)) throw new Exception("Expected to find entity on remove. A missed ADD perhaps?");

				_entities.Remove(entity);
			}
		}

		public Entity GetEntity(int entityId)
		{
			Entity player = _entities.FirstOrDefault(entity => entity.EntityId == entityId);

			return player;
		}
	}
}