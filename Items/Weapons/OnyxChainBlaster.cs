using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class OnyxChainBlaster : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/OnyxChainBlaster");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Onyx Chain Blaster");
        Item.damage = 47;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 64;
        Item.height = 32;
        ////Tooltip.SetDefault("Fires onyx blasts at incredible speeds\n50% chance to not consume ammo");
        Item.useTime = 10;
        Item.useAnimation = 10;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 4.5f;
        Item.value = 1750000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item36;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 24f;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(-5, 0);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-25, 26) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-25, 26) * 0.05f;
	    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), 661, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    for (int i = 0; i <= 3; i++)
	    {
	    	float SpeedNewX = velocity.X + (float) Main.rand.Next(-45, 46) * 0.05f;
	    	float SpeedNewY = velocity.Y + (float) Main.rand.Next(-45, 46) * 0.05f;
	    	Projectile.NewProjectile(source, position.X, position.Y, SpeedNewX, SpeedNewY, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    }
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 50)
    		return false;
    	return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.OnyxBlaster);
        recipe.AddIngredient(ItemID.ChainGun);
        recipe.AddIngredient(ItemID.LunarBar, 5);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}