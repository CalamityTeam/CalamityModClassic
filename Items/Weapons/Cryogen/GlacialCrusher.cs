using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.NPCs;

namespace CalamityModClassicPreTrailer.Items.Weapons.Cryogen
{
    public class GlacialCrusher : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glacial Crusher");
			/* Tooltip.SetDefault("Fires very slow frost projectiles that gain strength as they travel and freeze enemies\n" +
				"Enemies are frozen for longer the further the projectile travels\n" +
				"True melee strikes cause tremendous damage to frozen enemies\n" +
				"Enemies that cannot be frozen take increased damage"); */
		}

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.damage = 59;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 27;
            Item.useTime = 27;
            Item.useTurn = true;
            Item.useStyle = 1;
            Item.knockBack = 5f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 58;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
            Item.shoot = Mod.Find<ModProjectile>("Iceberg").Type;
            Item.shootSpeed = 3f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CryoBar", 10);
            recipe.AddTile(TileID.IceMachine);
            recipe.Register();
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.Next(3) == 0)
            {
                int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 67);
            }
        }

		public override void ModifyHitNPC(Player player, NPC target, ref NPC.HitModifiers modifiers)
		{
			if (target.buffImmune[Mod.Find<ModBuff>("GlacialState").Type])
			{
				modifiers.SourceDamage.Base *= 2;
				modifiers.Knockback *= 2f;
			}
			else if (target.GetGlobalNPC<CalamityGlobalNPC>().gState)
			{
				modifiers.SourceDamage.Base *= 3;
				modifiers.Knockback *= 3f;
			}
		}
    }
}
