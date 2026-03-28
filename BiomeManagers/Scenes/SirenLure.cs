using Microsoft.Xna.Framework;
using CalamityModClassicPreTrailer.NPCs.Leviathan;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.BiomeManagers.Scenes
{
    public class SirenLure : ModSceneEffect
    { 
        public override SceneEffectPriority Priority => SceneEffectPriority.BossLow;
        public int NPCType => ModContent.NPCType<LeviathanStart>();
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/SirenLure");
        public virtual int MusicDistance => 1600;

        public virtual bool SetSceneEffect(Player player)
        {
            Rectangle screenRect = new Rectangle((int)Main.screenPosition.X, (int)Main.screenPosition.Y,
                Main.screenWidth, Main.screenHeight);
            int musicDistance = MusicDistance * 2;
            bool inList = false;
            foreach (NPC npc in Main.ActiveNPCs)
            {
                if (npc.type == NPCType)
                {
                    inList = true;
                }

            if (!inList)
                continue;
            Rectangle npcBox = new Rectangle((int)npc.Center.X - MusicDistance, (int)npc.Center.Y - MusicDistance,
                musicDistance, musicDistance);
            if (screenRect.Intersects(npcBox))
                return true;
            }
            return false;
        }
        public override bool IsSceneEffectActive(Player player) => SetSceneEffect(player);
    }
}