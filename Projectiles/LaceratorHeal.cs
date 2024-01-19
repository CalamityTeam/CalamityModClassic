using System;
using System.Collections.Generic;
using CalamityModClassic1Point2.Buffs;
using CalamityModClassic1Point2.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static Humanizer.In;

namespace CalamityModClassic1Point2.Projectiles
{
    public class LaceratorHeal : ModProjectile
    {
        public override string Texture => "CalamityModClassic1Point2/Projectiles/AegisBeam";
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.SoulDrain);
            //Projectile.aiStyle = -1; ?
        }

        public override void AI()
        {
            return;
            if (Main.myPlayer != Projectile.owner || Projectile.localAI[0] != 0f)
            {
                return;
            }
            Player player = Main.player[Projectile.owner];
            bool flag = false;
            Rectangle hitbox;
            for (int i = 0; i < 200; i++)
            {
                NPC nPC = Main.npc[i];
                if (nPC.active && !nPC.townNPC && !nPC.dontTakeDamage && !nPC.friendly)
                {
                    hitbox = Projectile.Hitbox;
                    if (hitbox.Intersects(nPC.Hitbox))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            if (!flag)
            {
                for (int j = 0; j < 255; j++)
                {
                    Player player2 = Main.player[j];
                    if (player2.active && player2.whoAmI != player.whoAmI && player2.hostile && !player2.immune && !player2.dead && player2.team != player.team)
                    {
                        hitbox = Projectile.Hitbox;
                        if (hitbox.Intersects(player2.Hitbox))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }
            if (flag)
            {
                player.CheckMana(player.inventory[player.selectedItem], -1, pay: true);
                Projectile.localAI[0] = 1f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<LaceratorBuff>(), 30);
        }
    }
}