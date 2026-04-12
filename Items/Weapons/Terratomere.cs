using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class Terratomere : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terratomere");
            /* Tooltip.SetDefault("Linked to the essence of Terraria\n" +
                               "Heals the player on enemy hits"); */
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.damage = 135;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 20;
            Item.useStyle = 1;
            Item.useTime = 20;
            Item.useTurn = true;
            Item.knockBack = 7f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 64;
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("TerratomereProjectile").Type;
            Item.shootSpeed = 20f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int num6 = Main.rand.Next(4, 6);
            for (int index = 0; index < num6; ++index)
            {
                float num7 = velocity.X;
                float num8 = velocity.Y;
                float SpeedX = velocity.X + (float)Main.rand.Next(-40, 41) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-40, 41) * 0.05f;
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, (int)((double)damage * 0.5), knockback, player.whoAmI, 0.0f, 0.0f);
            }
            return false;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "XerocsGreatsword");
            recipe.AddIngredient(null, "Floodtide");
            recipe.AddIngredient(null, "Hellkite");
            recipe.AddIngredient(null, "TemporalFloeSword");
            recipe.AddIngredient(ItemID.TerraBlade);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "XerocsGreatsword");
            recipe.AddIngredient(null, "Floodtide");
            recipe.AddIngredient(null, "Hellkite");
            recipe.AddIngredient(null, "TemporalFloeSword");
            recipe.AddIngredient(null, "TerraEdge");
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.Next(3) == 0)
            {
                target.AddBuff(Mod.Find<ModBuff>("GlacialState").Type, 300);
            }
            target.AddBuff(BuffID.CursedInferno, 680);
            target.AddBuff(BuffID.Frostburn, 620);
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(BuffID.Ichor, 300);
            if (target.type == NPCID.TargetDummy || !target.canGhostHeal)
            {
                return;
            }
            int healAmount = (Main.rand.Next(3) + 2);
            player.statLife += healAmount;
            player.HealEffect(healAmount);
        }
    }
}
