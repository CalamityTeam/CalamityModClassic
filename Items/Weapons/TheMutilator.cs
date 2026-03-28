using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
    public class TheMutilator : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("The Mutilator");
            /* Tooltip.SetDefault("Striking an enemy below 20% life will trigger a bloodsplosion\n" +
                               "Bloodsplosions cause hearts to drop that can be picked up to heal you"); */
        }

        public override void SetDefaults()
        {
            Item.width = 80;
            Item.damage = 950;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 18;
            Item.useStyle = 1;
            Item.useTime = 18;
            Item.useTurn = true;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 80;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shootSpeed = 10f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (target.life <= (target.lifeMax * 0.2f) && target.canGhostHeal)
            {
                int heartDrop = Main.rand.Next(1, 3);
                for (int i = 0; i < heartDrop; i++)
                {
                    Item.NewItem(Entity.GetSource_FromThis(null), (int)target.position.X, (int)target.position.Y, target.width, target.height, 58, 1, false, 0, false, false);
                }
                SoundEngine.PlaySound(SoundID.Item14, target.position);
                target.position.X = target.position.X + (float)(target.width / 2);
                target.position.Y = target.position.Y + (float)(target.height / 2);
                target.position.X = target.position.X - (float)(target.width / 2);
                target.position.Y = target.position.Y - (float)(target.height / 2);
                for (int num621 = 0; num621 < 30; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 5, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 50; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 5, 0f, 0f, 100, default(Color), 3f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 5, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].velocity *= 2f;
                }
            }
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "BloodstoneCore", 5);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.Register();
        }
    }
}
