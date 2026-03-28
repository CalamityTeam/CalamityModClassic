using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TrueCausticEdge : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("True Caustic Edge");
            // Tooltip.SetDefault("Pestilent Defilement");
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.damage = 42;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 28;
            Item.useStyle = 1;
            Item.useTime = 28;
            Item.useTurn = true;
            Item.knockBack = 5.75f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 74;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("TrueCausticEdgeProjectile").Type;
            Item.shootSpeed = 16f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CausticEdge");
            recipe.AddIngredient(ItemID.FlaskofCursedFlames, 5);
            recipe.AddIngredient(ItemID.FlaskofPoison, 5);
            recipe.AddIngredient(ItemID.Deathweed, 3);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(null, "CausticEdge");
            recipe.AddIngredient(ItemID.FlaskofIchor, 5);
            recipe.AddIngredient(ItemID.FlaskofPoison, 5);
            recipe.AddIngredient(ItemID.Deathweed, 3);
            recipe.AddTile(TileID.DemonAltar);
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
			target.AddBuff(BuffID.Poisoned, 600);
			target.AddBuff(BuffID.OnFire, 300);
            target.AddBuff(BuffID.Venom, 300);
        }
    }
}
