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
	public class FeralthornClaymore : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Feralthorn Claymore");
		}

		public override void SetDefaults()
		{
			Item.width = 66;
			Item.damage = 63;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.useAnimation = 13;
			Item.useStyle = 1;
			Item.useTime = 13;
			Item.useTurn = true;
			Item.knockBack = 7.25f;
			Item.UseSound = SoundID.Item8;
			Item.autoReuse = true;
			Item.height = 66;
            Item.value = Item.buyPrice(0, 60, 0, 0);
            Item.rare = 7;
		}
	
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "DraedonBar", 12);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
		}

        public override void MeleeEffects(Player player, Rectangle hitbox)
	    {
	        if (Main.rand.Next(4) == 0)
	        {
	        	int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 44);
	        }
	    }
	    
	    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.Venom, 200);
		}
	}
}
