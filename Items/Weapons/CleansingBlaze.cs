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
	public class CleansingBlaze : ModItem
	{
		public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	    {
		    //texture =("CalamityModClassic1Point1/Items/Weapons/CleansingBlaze");
		    return true;
	    }
	
	    public override void SetDefaults()
	    {
		    //Tooltip.SetDefault("Cleansing Blaze");
			Item.damage = 236;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 62;
			Item.height = 38;
			////Tooltip.SetDefault("90% chance to not consume gel");
			Item.useTime = 3;
			Item.useAnimation = 12;
			Item.useStyle = 5;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 4f;
			Item.UseSound = SoundID.Item34;
			Item.value = 1350000;
			Item.rare = 10;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("EssenceFire").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 14f;
			Item.useAmmo = 23;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	        int num6 = Main.rand.Next(2, 4);
	        for (int index = 0; index < num6; ++index)
	        {
	            float SpeedX = velocity.X + (float) Main.rand.Next(-15, 16) * 0.05f;
	            float SpeedY = velocity.Y + (float) Main.rand.Next(-15, 16) * 0.05f;
	            Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY),type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	return false;
		}
	    
	    public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	    
	    public override bool CanConsumeAmmo(Item ammo, Player player)
	    {
	    	if (Main.rand.Next(0, 100) <= 90)
	    		return false;
	    	return true;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "CosmiliteBar", 12);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}