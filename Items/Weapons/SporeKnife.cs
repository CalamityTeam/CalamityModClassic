using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Projectiles;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class SporeKnife : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Spore Knife");
            // Tooltip.SetDefault("Enemies release spore clouds on death");
        }

        public override void SetDefaults()
        {
            Item.useStyle = 3;
            Item.useTurn = false;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.width = 28;
            Item.height = 28;
            Item.damage = 33;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.knockBack = 5.75f;
            Item.UseSound = SoundID.Item1;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.JungleSpores, 10);
            recipe.AddIngredient(ItemID.Stinger, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 2);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life <= 0)
            {
                int proj = Projectile.NewProjectile(Entity.GetSource_FromThis(null), target.Center.X, target.Center.Y, 0f, 0f, Main.rand.Next(569, 572), (int)((float)Item.damage * player.GetDamage(DamageClass.Melee).Multiplicative), Item.knockBack, Main.myPlayer);
				Main.projectile[proj].GetGlobalProjectile<CalamityGlobalProjectile>().forceMelee = true;
			}
        }
    }
}
