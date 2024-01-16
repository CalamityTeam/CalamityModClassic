using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons
{
	public class Helstorm : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/Helstorm");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Helstorm");
			Item.damage = 33;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 70;
			Item.height = 36;
			Item.useTime = 7;
			Item.useAnimation = 7;
			Item.useStyle = 5;
			Item.knockBack = 2.5f;
			Item.value = 420000;
			Item.rare = 7;
			Item.UseSound = SoundID.Item11;
			Item.autoReuse = true;
			Item.shoot = 10; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 11.5f;
			Item.useAmmo = 97;
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{    
		    int num6 = Main.rand.Next(2, 3);
		    for (int index = 0; index < num6; ++index)
		    {
		        float num7 = velocity.X;
		        float num8 = velocity.Y;
		        float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
		        float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
		        Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
		    }
		    return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "CruptixBar", 7);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
		
		public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	    {
			target.AddBuff(BuffID.OnFire, 400);
		}
	}
}