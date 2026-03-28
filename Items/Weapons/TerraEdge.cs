using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TerraEdge : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Edge");
            // Tooltip.SetDefault("Heals the player on enemy hits");
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.damage = 112;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 17;
            Item.useStyle = 1;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.knockBack = 6.25f;
            Item.UseSound = SoundID.Item60;
            Item.autoReuse = true;
            Item.height = 58;
            Item.value = Item.buyPrice(0, 80, 0, 0);
            Item.rare = 8;
            Item.shoot = Mod.Find<ModProjectile>("TerraEdgeBeam").Type;
            Item.shootSpeed = 12f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "TrueBloodyEdge");
            recipe.AddIngredient(ItemID.TrueExcalibur);
            recipe.AddIngredient(null, "LivingShard", 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.TrueNightsEdge);
            recipe.AddIngredient(ItemID.TrueExcalibur);
            recipe.AddIngredient(null, "LivingShard", 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(5) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 74);
            }
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.type == NPCID.TargetDummy || !target.canGhostHeal)
            {
                return;
            }
            int healAmount = (Main.rand.Next(2) + 2);
            player.statLife += healAmount;
            player.HealEffect(healAmount);
        }
    }
}
