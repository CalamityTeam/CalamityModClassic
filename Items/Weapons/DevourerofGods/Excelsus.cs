using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons.DevourerofGods
{
    public class Excelsus : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Excelsus");
            // Tooltip.SetDefault("Summons laser fountains on enemy hits");
        }

        public override void SetDefaults()
        {
            Item.width = 78;
            Item.damage = 235;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.useAnimation = 15;
            Item.useStyle = 1;
            Item.useTime = 15;
            Item.useTurn = true;
            Item.knockBack = 8f;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.height = 94;
            Item.value = Item.buyPrice(1, 40, 0, 0);
            Item.rare = 10;
            Item.shoot = Mod.Find<ModProjectile>("Excelsus").Type;
            Item.shootSpeed = 12f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 13;
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Vector2 origin = new Vector2(39f, 47f);
			spriteBatch.Draw(ModContent.Request<Texture2D>("CalamityModClassicPreTrailer/Items/Weapons/DevourerofGods/ExcelsusGlow").Value, Item.Center - Main.screenPosition, null, Color.White, rotation, origin, 1f, SpriteEffects.None, 0f);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int index = 0; index < 3; ++index)
            {
                float SpeedX = velocity.X + (float)Main.rand.Next(-30, 31) * 0.05f;
                float SpeedY = velocity.Y + (float)Main.rand.Next(-30, 31) * 0.05f;
				switch (index)
				{
					case 0:
						type = Mod.Find<ModProjectile>("Excelsus").Type;
						break;
					case 1:
						type = Mod.Find<ModProjectile>("ExcelsusBlue").Type;
						break;
					case 2:
						type = Mod.Find<ModProjectile>("ExcelsusPink").Type;
						break;
				}
                Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI, 0f, 0f);
            }
            return false;
        }

		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.NewProjectile(player.GetSource_ItemUse(Item), target.Center.X, target.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("LaserFountain").Type, 0, 0, Main.myPlayer);
        }
    }
}
